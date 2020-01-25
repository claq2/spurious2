using Spurious2.Core.LcboImporting.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spurious2.SqlRepositories.LcboImporting.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly SqlConnection connection;

        public StoreRepository(SqlConnection connection)
        {
            this.connection = connection;
        }

        public async Task Import(IEnumerable<StoreInfo> storeInfos)
        {
            if (storeInfos == null)
            {
                throw new ArgumentNullException(nameof(storeInfos));
            }

            await this.connection.OpenAsync().ConfigureAwait(false);
            try
            {
                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM StoreIncoming";
                    await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                }

                foreach (var storeInfo in storeInfos)
                {
                    using (var command = this.connection.CreateCommand())
                    {
                        command.CommandText = @"insert into StoreIncoming (id, city, storename, latitude, longitude) 
                                                values (@id, @city, @storename, @latitude, @longitude)";
                        var idParam = new SqlParameter("@id", storeInfo.LocationNumber);
                        var cityParam = new SqlParameter("@city", storeInfo.LocationCityName);
                        var storeNameParam = new SqlParameter("@storename", storeInfo.LocationName);
                        var latNameParam = new SqlParameter("@latitude", storeInfo.Latitude);
                        var longNameParam = new SqlParameter("@longitude", storeInfo.Longitude);

                        command.Parameters.Add(idParam);
                        command.Parameters.Add(cityParam);
                        command.Parameters.Add(storeNameParam);
                        command.Parameters.Add(latNameParam);
                        command.Parameters.Add(longNameParam);
                        await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                    }
                }

                // Call sproc to update table and clear incoming table
                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "UpdateStoresFromIncoming";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 60000;
                    await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
            }
            finally
            {
                this.connection.Close();
            }
        }

        public async Task UpdateStoreVolumes()
        {
            await this.connection.OpenAsync().ConfigureAwait(false);
            try
            {
                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "UpdateStoreVolumes";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 60000;
                    await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
            }
            finally
            {
                this.connection.Close();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.connection?.Dispose();
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
