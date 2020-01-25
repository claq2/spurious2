using ServiceStack.Data;
using ServiceStack.OrmLite;
using Spurious2.Core.Reading.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spurious2.OrmLite.Reading.Repositories
{
    public class SubdivisionRepository : ISubdivisionRepository
    {
        private static readonly Dictionary<AlcoholType, string> AlcoholTypeToDensityColumnMap = new Dictionary<AlcoholType, string>
        {
            { AlcoholType.All, "alcohol_density" },
            { AlcoholType.Beer, "beer_density" },
            { AlcoholType.Wine, "wine_density" },
            { AlcoholType.Spirits, "spirits_density" },
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
                return conn.Scalar<string>(@"SELECT ST_AsGeoJSON(""boundary"", 15, 4) as ""boundary""
                            from ""subdivisions""
                            where ""id"" = @id", new { id });
            }
        }

        public List<Subdivision> GetSubdivisionsForDensity(AlcoholType alcoholType, EndOfDistribution endOfDistribution, int limit)
        {
            var result = new List<Subdivision>();
            using (var conn = this.connectionFactory.Open())
            {
                result.AddRange(conn.SqlList<Subdivision>(@"SELECT * FROM ""get_densities""(@alcoholType, @sortOrder, @limit)", new { alcoholType = AlcoholTypeToDensityColumnMap[alcoholType], sortOrder = DistributionToSortOrderMap[endOfDistribution], limit }));
            }

            return result;
        }
    }
}
