using NetTopologySuite.IO;
using NetTopologySuite.IO.Converters;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Spurious2.Core.Reading.Domain;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Spurious2.SqlOrmLite.Reading.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly IDbConnectionFactory connectionFactory;
        private readonly JsonSerializerOptions jsonOptions;

        public StoreRepository(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
            jsonOptions = new JsonSerializerOptions { ReadCommentHandling = JsonCommentHandling.Skip };
            jsonOptions.Converters.Add(new GeoJsonConverterFactory());
        }

        public IEnumerable<Store> GetStoresForSubdivision(int subdivisionId)
        {
            var result = new List<Store>();

            using (var conn = this.connectionFactory.Open())
            {
                var sqlStores = conn.SqlList<SqlStore>(@"select s.id, 
                    s.storename as Name, 
                    s.beervolume, 
                    s.winevolume, 
                    s.spiritsvolume,
                    cast(s.location as nvarchar(max)) as LocationGeog
                    from store s ,Subdivision sd
                    where sd.boundary.STIntersects(s.location) = 1 and sd.id = @subdivisionId", new { subdivisionId });

                foreach (var sqlStore in sqlStores)
                {
                    var geogReader = new WKTReader();
                    var point = geogReader.Read(sqlStore.LocationGeog);
                    using (var memStream = new MemoryStream())
                    {
                        using (var writer = new Utf8JsonWriter(memStream))
                        {
                            JsonSerializer.Serialize(writer, point, jsonOptions);
                        }

                        var pointJson = Encoding.UTF8.GetString(memStream.ToArray());
                        result.Add(new Store
                        {
                            BeerVolume = sqlStore.BeerVolume,
                            Id = sqlStore.Id,
                            Location = pointJson,
                            Name = sqlStore.Name,
                            SpiritsVolume = sqlStore.SpiritsVolume,
                            WineVolume = sqlStore.WineVolume,
                        });
                    }
                }

                return result;
            }
        }

        [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Late bound")]
        private class SqlStore : Store
        {
            public string LocationGeog { get; set; }
        }
    }
}
