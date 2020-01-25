using GeoJSON.Net.Contrib.MsSqlSpatial;
using GeoJSON.Net.Geometry;
using Microsoft.SqlServer.Types;
using Newtonsoft.Json;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Spurious2.Core.Reading.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public SubdivisionRepository(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public string GetBoundaryForSubdivision(int id)
        {
            using (var conn = this.connectionFactory.Open())
            {
                var boundaryGeog = conn.Scalar<SqlGeography>(@"SELECT [Boundary]
                            FROM [Subdivision]
                            WHERE [Id] = @id", new { id });
                var tos = boundaryGeog.ToString();
                var s = tos.StartsWith("POLYGON", StringComparison.InvariantCulture) ?
                    JsonConvert.SerializeObject(boundaryGeog.ToGeoJSONObject<Polygon>()) :
                    JsonConvert.SerializeObject(boundaryGeog.ToGeoJSONObject<MultiPolygon>());
                return s;
            }
        }

        public List<Subdivision> GetSubdivisionsForDensity(AlcoholType alcoholType, EndOfDistribution endOfDistribution, int limit)
        {
            var result = new List<Subdivision>();
            using (var conn = this.connectionFactory.Open())
            {
                result.AddRange(
                    conn.SqlList<SqlSubdivision>(
@"SELECT TOP (" + limit + @") [Id]
    , [SubdivisionName] as [Name]
    , [GeographicCentre] as [CentreGeog]
    , [" + AlcoholTypeToDensityColumnMap[alcoholType] + @"] as [Density]
    , [Population]
    , 0 as [Volume]
FROM [Subdivision]
WHERE [AlcoholDensity] > 0
ORDER BY [Density] " + DistributionToSortOrderMap[endOfDistribution]
                    )
                    .Select(s => new Subdivision
                    {
                        Centre = JsonConvert.SerializeObject(s.CentreGeog.ToGeoJSONObject<Point>()),
                        Density = s.Density,
                        Id = s.Id,
                        Name = s.Name,
                        Population = s.Population,
                        Volume = s.Volume,
                    })
                    );
            }

            return result;
        }

        [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Late bound")]
        private class SqlSubdivision : Subdivision
        {
            public SqlGeography CentreGeog { get; set; }
        }
    }
}
