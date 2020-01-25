using ServiceStack.Configuration;
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
    public class StoreRepository : IStoreRepository
    {
        private readonly IDbConnectionFactory connectionFactory;

        public StoreRepository(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public IEnumerable<Store> GetStoresForSubdivision(int subdivisionId)
        {
            using (var conn = this.connectionFactory.Open())
            {
                return conn.SqlList<Store>(@"select s.id, s.name, s.beer_volume, s.wine_volume, s.spirits_volume, ST_AsGeoJSON(s.location) as location
                            from stores s
                             inner join subdivisions sd on ST_Intersects(ST_Transform(s.location::geometry, 4326), sd.boundary)
                             where sd.id = @subdivisionId", new { subdivisionId });
            }
        }
    }
}
