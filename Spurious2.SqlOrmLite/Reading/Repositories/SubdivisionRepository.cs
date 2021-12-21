using NetTopologySuite.IO;
using NetTopologySuite.IO.Converters;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Spurious2.Core.Reading.Domain;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Spurious2.SqlOrmLite.Reading.Repositories
{
    public class SubdivisionRepository : ISubdivisionRepository
    {
        private static readonly Dictionary<AlcoholType, string> AlcoholTypeToDensityColumnMap = new Dictionary<AlcoholType, string>
        {
            { AlcoholType.All, "AlcoholDensity" },
            { AlcoholType.Beer, "BeerDensity" },
            { AlcoholType.Wine, "WineDensity" },
            { AlcoholType.Spirits, "SpiritsDensity" },
        };

        private static readonly Dictionary<EndOfDistribution, string> DistributionToSortOrderMap = new Dictionary<EndOfDistribution, string>
        {
            { EndOfDistribution.Top, "DESC" },
            { EndOfDistribution.Bottom, "ASC" },
        };

        private readonly IDbConnectionFactory connectionFactory;
        private readonly JsonSerializerOptions jsonOptions;

        public SubdivisionRepository(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
            jsonOptions = new JsonSerializerOptions { ReadCommentHandling = JsonCommentHandling.Skip };
            jsonOptions.Converters.Add(new GeoJsonConverterFactory());
        }

        public string GetBoundaryForSubdivision(int id)
        {
            using (var conn = this.connectionFactory.Open())
            {
                var boundaryGeog = conn.Scalar<string>(@"SELECT cast([Boundary] as nvarchar(max)) as [Boundry]
                            FROM [Subdivision]
                            WHERE [Id] = @id", new { id });
                var geogReader = new WKTReader();
                var shape = geogReader.Read(boundaryGeog);

                using (var memStream = new MemoryStream())
                {
                    using (var writer = new Utf8JsonWriter(memStream))
                    {
                        JsonSerializer.Serialize(writer, shape, jsonOptions);
                    }

                    var shapeJson = Encoding.UTF8.GetString(memStream.ToArray());
                    return shapeJson;
                }
            }
        }

        public List<Subdivision> GetSubdivisionsForDensity(AlcoholType alcoholType, EndOfDistribution endOfDistribution, int limit)
        {
            var result = new List<Subdivision>();
            using (var conn = this.connectionFactory.Open())
            {
                var query = @"SELECT TOP (" + limit + @") [Id]
    , [SubdivisionName] as [Name]
    , cast([GeographicCentre] as nvarchar(max)) as [CentreGeog]
    , [" + AlcoholTypeToDensityColumnMap[alcoholType] + @"] as [Density]
    , [Population]
    , 0 as [Volume]
FROM [Subdivision]
WHERE [AlcoholDensity] > 0
ORDER BY [Density] " + DistributionToSortOrderMap[endOfDistribution];

                var sqlSubdivs = conn.SqlList<SqlSubdivision>(query);

                foreach (var sqlSubdiv in sqlSubdivs)
                {
                    var geogReader = new WKTReader();
                    var point = geogReader.Read(sqlSubdiv.CentreGeog);
                    using (var memStream = new MemoryStream())
                    {
                        using (var writer = new Utf8JsonWriter(memStream))
                        {
                            JsonSerializer.Serialize(writer, point, jsonOptions);
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
            }

            return result;
        }

        [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Late bound")]
        private class SqlSubdivision : Subdivision
        {
            public string CentreGeog { get; set; }
        }
    }
}
