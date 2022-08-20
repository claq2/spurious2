using NetTopologySuite.IO;
using NetTopologySuite.IO.Converters;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Spurious2.Core;
using Spurious2.Core.Reading.Domain;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;

namespace Spurious2.SqlOrmLite.Reading.Repositories;

public class SubdivisionRepository : ISubdivisionRepository
{
    private static readonly Dictionary<AlcoholType, string> AlcoholTypeToDensityColumnMap = new()
    {
        { AlcoholType.All, "AlcoholDensity" },
        { AlcoholType.Beer, "BeerDensity" },
        { AlcoholType.Wine, "WineDensity" },
        { AlcoholType.Spirits, "SpiritsDensity" },
    };

    private static readonly Dictionary<EndOfDistribution, string> DistributionToSortOrderMap = new()
    {
        { EndOfDistribution.Top, "DESC" },
        { EndOfDistribution.Bottom, "ASC" },
    };

    private readonly IDbConnectionFactory connectionFactory;
    private readonly JsonSerializerOptions jsonOptions;

    public SubdivisionRepository(IDbConnectionFactory connectionFactory)
    {
        this.connectionFactory = connectionFactory;
        this.jsonOptions = new JsonSerializerOptions { ReadCommentHandling = JsonCommentHandling.Skip };
        this.jsonOptions.Converters.Add(new GeoJsonConverterFactory());
    }

    public async Task<string> GetBoundaryForSubdivision(int id)
    {
        using var conn = await this.connectionFactory.OpenAsync().ConfigAwait();
        var boundaryGeog = await conn.ScalarAsync<string>(@"SELECT cast([Boundary] as nvarchar(max)) as [Boundry]
                            FROM [Subdivision]
                            WHERE [Id] = @id", new { id }).ConfigAwait();
        var geogReader = new WKTReader();
        var shape = geogReader.Read(boundaryGeog);

        using var memStream = new MemoryStream();
        using var writer = new Utf8JsonWriter(memStream);
        JsonSerializer.Serialize(writer, shape, this.jsonOptions);
        var shapeJson = Encoding.UTF8.GetString(memStream.ToArray());
        return shapeJson;
    }

    public async Task<List<Subdivision>> GetSubdivisionsForDensity(AlcoholType alcoholType, EndOfDistribution endOfDistribution, int limit)
    {
        var result = new List<Subdivision>();
        using var conn = await this.connectionFactory.OpenAsync().ConfigAwait();
        var query = @"SELECT TOP (" + limit + @") [Id]
    , [SubdivisionName] as [Name]
    , cast([GeographicCentre] as nvarchar(max)) as [CentreGeog]
    , [" + AlcoholTypeToDensityColumnMap[alcoholType] + @"] as [Density]
    , [Population]
    , 0 as [Volume]
FROM [Subdivision]
WHERE [AlcoholDensity] > 0
ORDER BY [Density] " + DistributionToSortOrderMap[endOfDistribution];

        var sqlSubdivs = await conn.SqlListAsync<SqlSubdivision>(query).ConfigAwait();

        foreach (var sqlSubdiv in sqlSubdivs)
        {
            var geogReader = new WKTReader();
            var point = geogReader.Read(sqlSubdiv.CentreGeog);
            using (var memStream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(memStream))
                {
                    JsonSerializer.Serialize(writer, point, this.jsonOptions);
                }

                var pointJson = Encoding.UTF8.GetString(memStream.ToArray());
                result.Add(new Subdivision
                {
                    Centre = pointJson,
                    Density = sqlSubdiv.Density,
                    Id = sqlSubdiv.Id,
                    Name = sqlSubdiv.Name,
                    Population = sqlSubdiv.Population,
                    Volume = sqlSubdiv.Volume,
                });
            }
        }

        return result;
    }

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Late bound")]
    private class SqlSubdivision : Subdivision
    {
        public string CentreGeog { get; set; } = string.Empty;
    }
}
