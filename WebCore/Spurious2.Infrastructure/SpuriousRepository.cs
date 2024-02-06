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

public class SpuriousRepository(Models.SpuriousContext dbContext) : ISpuriousRepository
{
    private static readonly JsonSerializerOptions jsonOptions = new() { ReadCommentHandling = JsonCommentHandling.Skip };

    private static readonly Dictionary<AlcoholType, Expression<Func<Subdivision, decimal?>>> map = new()
    {
        { AlcoholType.All, s => s.AlcoholDensity },
        { AlcoholType.Beer, s => s.BeerDensity },
        { AlcoholType.Spirits, s => s.SpiritsDensity },
        { AlcoholType.Wine, s => s.WineDensity },
    };

    static SpuriousRepository()
    {
        jsonOptions.Converters.Add(new GeoJsonConverterFactory());
    }

    public async Task<List<Store>> GetStoresBySubdivisionId(int subdivisionId)
    {
        var stores = await dbContext
            .Stores
            .Where(s => s.LocationGeog.Intersects(
                dbContext.Subdivisions
                .Single(s => s.Id == subdivisionId).Boundary
            )).ToListAsync()
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

    public async Task<string> GetBoundaryForSubdivision(int subdivisionId)
    {
        var subdiv = await dbContext.Subdivisions
            .SingleAsync(s => s.Id == subdivisionId)
            .ConfigAwait();
        using var memStream = new MemoryStream();
        using var writer = new Utf8JsonWriter(memStream);
        JsonSerializer.Serialize(writer, subdiv.Boundary, jsonOptions);
        var shapeJson = Encoding.UTF8.GetString(memStream.ToArray());
        return shapeJson;
    }

    public async Task<List<Subdivision>> GetSubdivisionsForDensity(AlcoholType alcoholType, EndOfDistribution endOfDistribution, int limit)
    {
        var keySelector = map[alcoholType];
        var subdivsQuery = dbContext.Subdivisions
            .Where(s => s.AlcoholDensity > 0);
        subdivsQuery = DetermineOrderQuery(subdivsQuery, keySelector, endOfDistribution)
            .Take(limit);

        var subdivs = await subdivsQuery.ToListAsync().ConfigAwait();

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

    private static decimal GetRequestedDensityAmount(Subdivision subdivision, AlcoholType alcoholType)
    {
        decimal result = 0;
        if (alcoholType == AlcoholType.All)
        {
            result = subdivision.AlcoholDensity ?? 0;
        }
        else if (alcoholType == AlcoholType.Beer)
        {
            result = subdivision.BeerDensity ?? 0;
        }
        else if (alcoholType == AlcoholType.Wine)
        {
            result = subdivision.WineDensity ?? 0;
        }
        else if (alcoholType == AlcoholType.Spirits)
        {
            result = subdivision.SpiritsDensity ?? 0;
        }

        return result;
    }
}
