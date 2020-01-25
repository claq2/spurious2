using Npgsql;
using Spurious2.Core.LcboImporting.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Spurious2.Repositories.LcboImporting.Repositories
{
    public class ProductRepository : IProductRepository
    {
        readonly NpgsqlConnection connection;

        public ProductRepository(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        public async Task Import(IEnumerable<Product> products)
        {
            if (products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            await this.connection.OpenAsync().ConfigureAwait(false);
            try
            {
                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM product_incoming";
                    await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                }

                using (var writer = this.connection.BeginBinaryImport("COPY product_incoming (id, name, category, volume) FROM STDIN (FORMAT BINARY)"))
                {
                    foreach (var product in products)
                    {
                        writer.StartRow();
                        writer.Write(product.ItemNumber, NpgsqlTypes.NpgsqlDbType.Integer);
                        writer.Write(product.ItemName, NpgsqlTypes.NpgsqlDbType.Text);
                        writer.Write(product.LiquorType, NpgsqlTypes.NpgsqlDbType.Text);
                        writer.Write(product.PackageVolume, NpgsqlTypes.NpgsqlDbType.Integer);
                    }

                    writer.Complete();
                }

                // Call sproc to update table and clear incoming table
                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "call update_products_from_incoming()";
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

        public async Task<IEnumerable<int>> GetProductIds()
        {
            var result = new List<int>();
            await this.connection.OpenAsync().ConfigureAwait(false);
            try
            {
                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "select id from products order by id asc";
                    using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            result.Add(Convert.ToInt32(reader["id"], CultureInfo.InvariantCulture));
                        }
                    }
                }
            }
            finally
            {
                this.connection.Close();
            }

            return result;
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
