using Spurious2.Core;
using Spurious2.Core.LcboImporting.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spurious2.SqlRepositories.LcboImporting.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly SqlConnection connection;
        private readonly Func<SqlConnection> connectionFactory;

        public InventoryRepository(SqlConnection connection, Func<SqlConnection> connectionFactory)
        {
            this.connection = connection;
            this.connectionFactory = connectionFactory;
        }

        public async Task ClearIncomingInventory()
        {
            await this.connection.OpenAsync().ConfigureAwait(false);
            try
            {
                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM InventoryIncoming";
                    command.CommandTimeout = 120000;
                    await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
            }
            finally
            {
                this.connection.Close();
            }
        }

        public async Task ClearInventoryPages()
        {
            await this.connection.OpenAsync().ConfigureAwait(false);
            try
            {
                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM InventoryPage";
                    command.CommandTimeout = 120000;
                    await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
            }
            finally
            {
                this.connection.Close();
            }
        }

        public async Task<string> GetHtmlForIdAsync(int productId)
        {
            using (var conn = connectionFactory.Invoke())
            {
                await conn.OpenAsync().ConfigureAwait(false);
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select compressedcontent FROM InventoryPage where ProductId = @pid";
                    var contentParam = new SqlParameter("pid", System.Data.DbType.Int32) { Value = productId };
                    command.Parameters.Add(contentParam);
                    command.CommandTimeout = 120000;
                    var res = await command.ExecuteScalarAsync().ConfigureAwait(false);
                    return ((byte[])res).Decompress();
                }
            }
        }

        public async Task Import(List<Inventory> inventories)
        {
            if (inventories == null)
            {
                throw new ArgumentNullException(nameof(inventories));
            }

            var dataTable = new DataTable(nameof(Inventory));
            dataTable.Columns.Add(nameof(Inventory.ProductId));
            dataTable.Columns.Add(nameof(Inventory.StoreId));
            dataTable.Columns.Add(nameof(Inventory.Quantity));
            foreach (var inventory in inventories)
            {
                var values = new object[3];
                values[0] = inventory.ProductId;
                values[1] = inventory.StoreId;
                values[2] = inventory.Quantity;

                dataTable.Rows.Add(values);
            }

            await this.connection.OpenAsync().ConfigureAwait(false);
            var transaction = this.connection.BeginTransaction();
            try
            {
                
                using (var bulkCopy = new SqlBulkCopy(this.connection, SqlBulkCopyOptions.Default, transaction))
                {
                    bulkCopy.DestinationTableName = "InventoryIncoming";
                    bulkCopy.BulkCopyTimeout = 120000;
                    bulkCopy.WriteToServer(dataTable);
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                this.connection.Close();
            }
        }

        public async Task ImportHtml(string invHtml, int pid)
        {
            using (var conn = this.connectionFactory.Invoke())
            {
                //conn.ConnectionTimeout = 90;
                await conn.OpenAsync().ConfigureAwait(false);
                //await this.connection.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    var compressed = invHtml.Compress();
                    command.CommandText = "insert into InventoryPage (compressedcontent, productId) values (@html, @productId)";
                    var contentParam = new SqlParameter("html", System.Data.DbType.Binary) { Value = compressed };
                    command.Parameters.Add(contentParam);
                    var productIdParam = new SqlParameter("productId", System.Data.DbType.Int32) { Value = pid };
                    command.Parameters.Add(productIdParam);
                    command.CommandTimeout = 120000;
                    await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
            }
        }

        public async Task UpdateInventoriesFromIncoming()
        {
            await this.connection.OpenAsync().ConfigureAwait(false);
            try
            {
                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "UpdateInventoriesFromIncoming";
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

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
