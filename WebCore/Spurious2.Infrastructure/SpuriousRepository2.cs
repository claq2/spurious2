using Microsoft.EntityFrameworkCore;
using NetTopologySuite.IO.Converters;
using Spurious2.Core;
using Spurious2.Core2.Stores;
using System.Text;
using System.Text.Json;

namespace Spurious2.Infrastructure;

public class SpuriousRepository2(Models.SpuriousContext dbContext) : ISpuriousRepository
{
    static readonly JsonSerializerOptions jsonOptions;
    static SpuriousRepository2()
    {
        jsonOptions = new JsonSerializerOptions { ReadCommentHandling = JsonCommentHandling.Skip };
        jsonOptions.Converters.Add(new GeoJsonConverterFactory());
    }

    public async Task<List<Store>> GetStoresBySubdivisionId(int subdivisionId)
    {
        var stores = await dbContext
            .Stores
            .Where(s => s.LocationGeog.Intersects(
                dbContext.Subdivisions
                .Single(s => s.Id == subdivisionId).Boundary
            )).ToListAsync();
        foreach (var store in stores)
        {
            using var memStream = new MemoryStream();
            using var writer = new Utf8JsonWriter(memStream);
            JsonSerializer.Serialize(writer, store.LocationGeog, jsonOptions);
            var pointJson = Encoding.UTF8.GetString(memStream.ToArray());
            store.Location = pointJson;
        }

        return stores;
    }

    public async Task<string> GetBoundaryForSubdivision(int subdivisionId)
    {
        var subdiv = await dbContext.Subdivisions.SingleAsync(s => s.Id == subdivisionId);
        using var memStream = new MemoryStream();
        using var writer = new Utf8JsonWriter(memStream);
        JsonSerializer.Serialize(writer, subdiv.Boundary, jsonOptions);
        var shapeJson = Encoding.UTF8.GetString(memStream.ToArray());
        return shapeJson;
    }
}
