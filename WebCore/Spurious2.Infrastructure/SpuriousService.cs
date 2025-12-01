using System.Text;
using System.Text.Json;
using Ardalis.Specification;
using GeoJSON.Text.Geometry;
using Microsoft.Extensions.Logging;
using Spurious2.Core2;
using Spurious2.Core2.Stores;
using Spurious2.Core2.Subdivisions;

namespace Spurious2.Infrastructure;

public class SpuriousService(IReadRepositoryBase<Subdivision> subdivisionRepository,
    IReadRepositoryBase<Store> storeRepository,
    ILogger<SpuriousService> logger)
{
    private static readonly List<Subdivision> inMemSubdivisions = [new()];
    private static readonly List<Store> inMemStores = [new()];
    private static readonly JsonSerializerOptions jsonOptions = new() { ReadCommentHandling = JsonCommentHandling.Skip };
    static SpuriousService()
    {
        // TODO: Load subdivisions and stores into in-memory list for fallback
    }

    public async Task<string> GetBoundaryForSubdivision(int subdivisionId, CancellationToken cancellationToken)
    {
        var spec = new BoundryBySubdivisionSpec(subdivisionId);
        NetTopologySuite.Geometries.Geometry? subdivGeom;
        try
        {
            subdivGeom = await subdivisionRepository.SingleOrDefaultAsync(
                    spec,
                    cancellationToken)
                    .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Error retrieving boundary for subdivision {SubdivisionId}, returning from in memory list", subdivisionId);
            subdivGeom = spec.Evaluate(inMemSubdivisions).SingleOrDefault();
        }

        using var memStream = new MemoryStream();
        using var writer = new Utf8JsonWriter(memStream);
        JsonSerializer.Serialize(writer, subdivGeom, jsonOptions);
        var shapeJson = Encoding.UTF8.GetString(memStream.ToArray());
        return shapeJson;
    }

    public async Task<List<Subdivision>> GetSubdivisionsForDensity(AlcoholType alcoholType,
        EndOfDistribution endOfDistribution,
        int limit,
        CancellationToken cancellationToken)
    {
        List<Subdivision>? subdivs;
        var spec = new SubdivisionsByDensitySpec(alcoholType, endOfDistribution, limit);
        try
        {
            subdivs = await subdivisionRepository.ListAsync(
                    spec,
                    cancellationToken)
                    .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Error retrieving subdivisions for density {AlcoholType} {EndOfDistribution}, returning from in memory list", alcoholType, endOfDistribution);
            subdivs = [.. spec.Evaluate(inMemSubdivisions)];
        }

        foreach (var subdiv in subdivs)
        {
            using var memStream = new MemoryStream();
            using var writer = new Utf8JsonWriter(memStream);
            JsonSerializer.Serialize(writer, subdiv.GeographicCentreGeog, jsonOptions);
            var pointJson = Encoding.UTF8.GetString(memStream.ToArray());
            subdiv.GeographicCentre = JsonSerializer.Deserialize<Point>(pointJson)
                ?? new Point();
            subdiv.RequestedDensityAmount = GetRequestedDensityAmount(subdiv, alcoholType) / 1000;
        }

        return subdivs;
    }

    public async Task<List<Store>> GetStoresBySubdivisionId(int subdivisionId, CancellationToken cancellationToken)
    {
        List<Store>? stores;
        var spec = new StoresBySubdivisionSpec(subdivisionId);
        try
        {
            stores = await storeRepository.ListAsync(spec, cancellationToken)
                .ConfigAwait();
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Error retrieving stores for subdivision {SubdivisionId}, returning from in memory list", subdivisionId);
            stores = [.. spec.Evaluate(inMemStores)];
        }

        foreach (var store in stores)
        {
            using var memStream = new MemoryStream();
            using var writer = new Utf8JsonWriter(memStream);
            JsonSerializer.Serialize(writer, store.LocationGeog, jsonOptions);
            var pointJson = Encoding.UTF8.GetString(memStream.ToArray());
            store.Location = JsonSerializer.Deserialize<Point>(pointJson)
                ?? new Point();
        }

        return stores;
    }

    private static decimal GetRequestedDensityAmount(Subdivision subdivision, AlcoholType alcoholType)
    {
        var result = alcoholType switch
        {
            AlcoholType.All => subdivision.AlcoholDensity ?? 0,
            AlcoholType.Beer => subdivision.BeerDensity ?? 0,
            AlcoholType.Wine => subdivision.WineDensity ?? 0,
            AlcoholType.Spirits => subdivision.SpiritsDensity ?? 0,
            _ => 0,
        };
        return result;
    }
}
