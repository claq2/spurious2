using GeoJSON.Net.Contrib.MsSqlSpatial;
using GeoJSON.Net.Geometry;
using Microsoft.SqlServer.Types;
using Newtonsoft.Json;
using ServiceStack.Configuration;
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
                var sqlStores = conn.SqlList<SqlStore>(@"select s.id, s.storename as Name, s.beervolume, s.winevolume, s.spiritsvolume, s.location as LocationGeog
                            from store s ,Subdivision sd
                            where sd.boundary.STIntersects(s.location) = 1 and sd.id = @subdivisionId", new { subdivisionId });
                return sqlStores.Select(s => new Store
                {
                    BeerVolume = s.BeerVolume,
                    Id = s.Id,
                    Location = JsonConvert.SerializeObject(s.LocationGeog.ToGeoJSONObject<Point>()),
                    Name = s.Name,
                    SpiritsVolume = s.SpiritsVolume,
                    WineVolume = s.WineVolume,
                });
            }
        }

        [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Late bound")]
        private class SqlStore : Store
        {
            public SqlGeography LocationGeog { get; set; }
        }
    }
}
