using Npgsql;
using Spurious2.Core.LcboImporting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spurious2.Repositories.LcboImporting.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        readonly NpgsqlConnection connection;

        public StoreRepository(NpgsqlConnection connection)
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
                    command.CommandText = "DELETE FROM store_incoming";
                    await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                }

                using (var writer = this.connection.BeginBinaryImport("COPY store_incoming (id, city, name, latitude, longitude) FROM STDIN (FORMAT BINARY)"))
                {
                    foreach (var storeInfo in storeInfos)
                    {
                        writer.StartRow();
                        writer.Write(storeInfo.LocationNumber, NpgsqlTypes.NpgsqlDbType.Integer);
                        writer.Write(storeInfo.LocationCityName, NpgsqlTypes.NpgsqlDbType.Text);
                        writer.Write(storeInfo.LocationName, NpgsqlTypes.NpgsqlDbType.Text);
                        writer.Write(storeInfo.Latitude, NpgsqlTypes.NpgsqlDbType.Numeric);
                        writer.Write(storeInfo.Longitude, NpgsqlTypes.NpgsqlDbType.Numeric);
                    }

                    writer.Complete();
                }

                // Call sproc to update table and clear incoming table
                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "call update_stores_from_incoming()";
                    command.CommandType = System.Data.CommandType.Text;
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
                    command.CommandText = "call update_store_volumes()";
                    command.CommandType = System.Data.CommandType.Text;
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

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
