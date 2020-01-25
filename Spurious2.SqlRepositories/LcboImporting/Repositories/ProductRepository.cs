using Spurious2.Core.LcboImporting.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spurious2.SqlRepositories.LcboImporting.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SqlConnection connection;

        public ProductRepository(SqlConnection connection)
        {
            this.connection = connection;
        }

        public async Task<IEnumerable<int>> GetProductIds()
        {
            var result = new List<int>();
            await this.connection.OpenAsync().ConfigureAwait(false);
            try
            {
                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "select id from product order by id asc";
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
                    command.CommandText = "DELETE FROM ProductIncoming";
                    await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                }

                foreach (var product in products)
                {
                    using (var command = this.connection.CreateCommand())
                    {
                        command.CommandText = @"insert into ProductIncoming (id, productname, category, volume) 
                                                values (@id, @productname, @category, @volume)";
                        var idParam = new SqlParameter("@id", product.ItemNumber);
                        var nameParam = new SqlParameter("@productname", product.ItemName);
                        var categoryParam = new SqlParameter("@category", product.LiquorType);
                        var volumeParam = new SqlParameter("@volume", product.PackageVolume);

                        command.Parameters.Add(idParam);
                        command.Parameters.Add(nameParam);
                        command.Parameters.Add(categoryParam);
                        command.Parameters.Add(volumeParam);
                        await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                    }
                }

                // Call sproc to update table and clear incoming table
                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "UpdateProductsFromIncoming";
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
