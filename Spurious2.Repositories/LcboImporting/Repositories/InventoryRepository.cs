using Npgsql;
using Spurious2.Core;
using Spurious2.Core.LcboImporting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spurious2.Repositories.LcboImporting.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        readonly NpgsqlConnection connection;
        readonly Func<NpgsqlConnection> connectionFactory;
        public InventoryRepository(NpgsqlConnection connection, Func<NpgsqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
            this.connection = connection;
        }

        public async Task ClearIncomingInventory()
        {
            await this.connection.OpenAsync().ConfigureAwait(false);
            try
            {
                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM inventory_incoming";
                    await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
            }
            finally
            {
                this.connection.Close();
            }
        }

        public async Task Import(List<Inventory> inventories)
        {
            if (inventories == null)
            {
                throw new ArgumentNullException(nameof(inventories));
            }

            await this.connection.OpenAsync().ConfigureAwait(false);
            try
            {
                using (var writer = this.connection.BeginBinaryImport("COPY inventory_incoming (product_id, store_id, quantity) FROM STDIN (FORMAT BINARY)"))
                {
                    foreach (var inventory in inventories)
                    {
                        if (inventory == null)
                        {
                            Console.WriteLine("Found null inventory");
                        }
                        else
                        {
                            writer.StartRow();
                            writer.Write(inventory.ProductId, NpgsqlTypes.NpgsqlDbType.Integer);
                            writer.Write(inventory.StoreId, NpgsqlTypes.NpgsqlDbType.Integer);
                            writer.Write(inventory.Quantity, NpgsqlTypes.NpgsqlDbType.Integer);
                        }
                    }

                    writer.Complete();
                }
            }
            finally
            {
                this.connection.Close();
            }
        }

        public async Task UpdateInventoriesFromIncoming()
        {
            await this.connection.OpenAsync().ConfigureAwait(false);
            try
            {
                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "call update_inventories_from_incoming()";
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

        public async Task ImportHtml(string invHtml, int pid)
        {
            using (var conn = this.connectionFactory.Invoke())
            {
                //conn.ConnectionTimeout = 90;
                await conn.OpenAsync().ConfigureAwait(false);
                using (var command = conn.CreateCommand())
                {
                    var compressed = invHtml.Compress();
                    command.CommandText = "insert into inventory_pages (compressed_content, product_id) values (@html, @productId)";
                    var contentParam = new NpgsqlParameter("html", System.Data.DbType.Binary) { Value = compressed };
                    command.Parameters.Add(contentParam);
                    var productIdParam = new NpgsqlParameter("productId", System.Data.DbType.Int32) { Value = pid };
                    command.Parameters.Add(productIdParam);
                    await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
            }
        }

        public async Task ClearInventoryPages()
        {
            await this.connection.OpenAsync().ConfigureAwait(false);
            try
            {
                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM inventory_pages";
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
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<string> GetHtmlForIdAsync(int productId)
        {
            using (var conn = connectionFactory.Invoke())
            {
                await conn.OpenAsync().ConfigureAwait(false);
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select compressed_content FROM inventory_pages where product_id = @pid";
                    var contentParam = new NpgsqlParameter("pid", System.Data.DbType.Int32) { Value = productId };
                    command.Parameters.Add(contentParam);
                    var res = await command.ExecuteScalarAsync().ConfigureAwait(false);
                    return ((byte[])res).Decompress();
                }
            }
        }
    }
}
