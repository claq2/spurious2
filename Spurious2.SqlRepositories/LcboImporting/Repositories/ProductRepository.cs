using Spurious2.Core;
using Spurious2.Core.LcboImporting.Domain;
using System.Data;
using System.Data.SqlClient;

namespace Spurious2.SqlRepositories.LcboImporting.Repositories;

public class ProductRepository(SqlConnection connection) : IProductRepository
{
    public async Task ImportAFew(IEnumerable<Product> products)
    {
        await connection.OpenAsync().ConfigAwait();
        try
        {
            using var command = connection.CreateCommand();
            command.CommandText = @"
                        insert into ProductIncoming (id, productname, category, volume, productdone)
                        select Id, ProductName, Category, Volume, ProductDone
                        from @products
                        where Id not in (select Id from ProductIncoming)";
            var param = command.Parameters.AddWithValue("@products", ToDataTable(products));
            param.TypeName = "dbo.IncomingProduct";
            param.SqlDbType = SqlDbType.Structured;
            await command.ExecuteNonQueryAsync().ConfigAwait();
        }
        finally
        {
            await connection.CloseAsync().ConfigAwait();
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            connection?.Dispose();
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task ClearIncomingProducts()
    {
        await connection.OpenAsync().ConfigAwait();
        try
        {
            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM ProductIncoming";
            await command.ExecuteNonQueryAsync().ConfigAwait();
        }
        finally
        {
            await connection.CloseAsync().ConfigAwait();
        }
    }

    public async Task MarkIncomingProductDone(string productId)
    {
        await connection.OpenAsync().ConfigAwait();
        try
        {
            using var command = connection.CreateCommand();
            command.CommandText = "UPDATE [ProductIncoming] SET [ProductDone] = 1 WHERE [Id] = @id";
            command.Parameters.AddWithValue("@id", Convert.ToInt32(productId));
            await command.ExecuteNonQueryAsync().ConfigAwait();
        }
        finally
        {
            await connection.CloseAsync().ConfigAwait();
        }
    }

    public async Task UpdateProductsFromIncoming()
    {
        await connection.OpenAsync().ConfigAwait();
        try
        {
            // Call sproc to update table and clear incoming table
            using var updateProductsCommand = connection.CreateCommand();
            updateProductsCommand.CommandText = "UpdateProductsFromIncoming";
            updateProductsCommand.CommandType = System.Data.CommandType.StoredProcedure;
            updateProductsCommand.CommandTimeout = 60000;
            await updateProductsCommand.ExecuteNonQueryAsync().ConfigAwait();
        }
        finally
        {
            await connection.CloseAsync().ConfigAwait();
        }
    }

    private static DataTable ToDataTable(IEnumerable<Product> products)
    {
        DataTable table = new();
        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("ProductName");
        table.Columns.Add("Category");
        table.Columns.Add("Volume", typeof(int));
        table.Columns.Add("ProductDone", typeof(bool));
        foreach (Product product2 in products)
        {
            table.Rows.Add(product2.Id, product2.Name, product2.LiquorType, product2.PackageVolume, true);
        }

        return table;
    }
}
