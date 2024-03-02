using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using GeoJSON.Text.Geometry;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.IO.Converters;
using Spurious2.Core2;
using Spurious2.Core2.Stores;
using Spurious2.Core2.Subdivisions;

namespace Spurious2.Infrastructure;

public class SpuriousRepository(All.SpuriousContext dbContext) : ISpuriousRepository
{
    private static readonly JsonSerializerOptions jsonOptions = new() { ReadCommentHandling = JsonCommentHandling.Skip };

    private static readonly Dictionary<AlcoholType, Expression<Func<Subdivision, decimal?>>> map = new()
    {
        { AlcoholType.All, s => s.AlcoholDensity },
        { AlcoholType.Beer, s => s.BeerDensity },
        { AlcoholType.Spirits, s => s.SpiritsDensity },
        { AlcoholType.Wine, s => s.WineDensity },
    };
    private bool _disposedValue;

    static SpuriousRepository() => jsonOptions.Converters.Add(new GeoJsonConverterFactory());

    public async Task<List<Store>> GetStoresBySubdivisionId(int subdivisionId, CancellationToken cancellationToken)
    {
        var stores = await dbContext
            .Stores
            .Where(s => s.LocationGeog.Intersects(
                dbContext.Subdivisions
                .Single(s => s.Id == subdivisionId).Boundary
            )).ToListAsync(cancellationToken)
            .ConfigAwait();
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

    public async Task<string> GetBoundaryForSubdivision(int subdivisionId, CancellationToken cancellationToken)
    {
        var subdiv = await dbContext.Subdivisions
            .SingleAsync(s => s.Id == subdivisionId, cancellationToken)
            .ConfigAwait();
        using var memStream = new MemoryStream();
        using var writer = new Utf8JsonWriter(memStream);
        JsonSerializer.Serialize(writer, subdiv.Boundary, jsonOptions);
        var shapeJson = Encoding.UTF8.GetString(memStream.ToArray());
        return shapeJson;
    }

    public async Task<List<Subdivision>> GetSubdivisionsForDensity(AlcoholType alcoholType,
        EndOfDistribution endOfDistribution,
        int limit,
        CancellationToken cancellationToken)
    {
        var keySelector = map[alcoholType];
        var subdivsQuery = dbContext.Subdivisions
            .Where(s => s.AlcoholDensity > 0);
        subdivsQuery = DetermineOrderQuery(subdivsQuery, keySelector, endOfDistribution)
            .Take(limit);

        var subdivs = await subdivsQuery.ToListAsync(cancellationToken).ConfigAwait();

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

        static IOrderedQueryable<Subdivision> DetermineOrderQuery(IQueryable<Subdivision> subdivsQuery, Expression<Func<Subdivision, decimal?>> keySelector, EndOfDistribution endOfDistribution)
            => endOfDistribution == EndOfDistribution.Top ?
                subdivsQuery.OrderByDescending(keySelector)
                : subdivsQuery.OrderBy(keySelector);
    }

    public async Task ImportBoundaries(IEnumerable<BoundaryIncoming> boundaries)
    {
        ArgumentNullException.ThrowIfNull(boundaries);

        _ = await dbContext.Database.ExecuteSqlAsync($"DELETE FROM BoundaryIncoming").ConfigAwait();

        foreach (var boundary in boundaries)
        {

            _ = await dbContext.Database.ExecuteSqlAsync($@"insert into boundaryincoming (id, 
[BoundaryWellKnownText], 
OriginalBoundary, 
ReorientedBoundary, 
SubdivisionName, province) 
                                                values ({boundary.Id}, 
{boundary.BoundaryWellKnownText},
geography::STGeomFromText({boundary.BoundaryWellKnownText}, 4326).MakeValid(), 
geography::STGeomFromText({boundary.BoundaryWellKnownText}, 4326).MakeValid().ReorientObject(), 
{boundary.SubdivisionName}, {boundary.Province})").ConfigAwait();
        }

        // Call sproc to update table and clear incoming table
        _ = await dbContext.Database.ExecuteSqlAsync($"UpdateBoundariesFromIncoming").ConfigAwait();
    }

    public async Task ImportStoresFromCsv(IEnumerable<StoreIncoming> stores)
    {
        // Stores in CSV file have volumes

        ArgumentNullException.ThrowIfNull(stores);

        _ = await dbContext.Database.ExecuteSqlAsync($"DELETE FROM storeincoming").ConfigAwait();

        foreach (var store in stores)
        {

            _ = await dbContext.Database.ExecuteSqlAsync($@"insert into storeincoming (id, 
[LocationWellKnownText],
Location,  
StoreName, City,
BeerVolume,
WineVolume,
SpiritsVolume,
StoreDone) 
                                                values ({store.Id}, 
{store.LocationWellKnownText},
geography::STPointFromText({store.LocationWellKnownText}, 4326), 
{store.StoreName}, {store.City},
{store.BeerVolume},
{store.WineVolume},
{store.SpiritsVolume},
1)").ConfigAwait();

        }

        // Call sproc to update tables
        _ = await dbContext.Database.ExecuteSqlAsync($"UpdateStoresFromIncomingCsv").ConfigAwait();
        _ = await dbContext.Database.ExecuteSqlAsync($"UpdateSubdivisionVolumes").ConfigAwait();
    }

    public async Task ImportPopulations(IEnumerable<PopulationIncoming> populations)
    {
        ArgumentNullException.ThrowIfNull(populations);

        _ = await dbContext.Database.ExecuteSqlAsync($"DELETE FROM PopulationIncoming").ConfigAwait();

        foreach (var subdivisionPopulation in populations)
        {
            _ = await dbContext.Database.ExecuteSqlAsync($@"insert into PopulationIncoming (id, population, Province) 
                                                values ({subdivisionPopulation.Id}, {subdivisionPopulation.Population}
                                                , {subdivisionPopulation.Province})").ConfigAwait();
        }

        // Call sproc to update table
        _ = await dbContext.Database.ExecuteSqlAsync($"UpdatePopulationsFromIncoming").ConfigAwait();
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

    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposedValue)
        {
            if (disposing)
            {
                dbContext?.Dispose();
            }

            this._disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
