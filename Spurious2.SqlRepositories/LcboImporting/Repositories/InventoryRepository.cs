using Microsoft.Extensions.Logging;
using Spurious2.Core;
using Spurious2.Core.LcboImporting.Domain;
using System.Data;
using System.Data.SqlClient;

namespace Spurious2.SqlRepositories.LcboImporting.Repositories;

public class InventoryRepository(SqlConnection connection, ILogger<StoreRepository> logger) : IInventoryRepository
{
    public async Task ClearIncomingInventory()
    {
        await connection.OpenAsync().ConfigAwait();
        try
        {
            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM InventoryIncoming";
            command.CommandTimeout = 120000;
            await command.ExecuteNonQueryAsync().ConfigAwait();
            logger.LogInformation("Cleared InventoryIncoming table");
        }
        finally
        {
            await connection.CloseAsync().ConfigAwait();
        }
    }

    public async Task ClearInventoryPages()
    {
        await connection.OpenAsync().ConfigAwait();
        try
        {
            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM InventoryPage";
            command.CommandTimeout = 120000;
            await command.ExecuteNonQueryAsync().ConfigAwait();
        }
        finally
        {
            await connection.CloseAsync().ConfigAwait();
        }
    }

    public async Task UpdateInventoriesFromIncoming()
    {
        await connection.OpenAsync().ConfigAwait();
        try
        {
            using var command = connection.CreateCommand();
            command.CommandText = "UpdateInventoriesFromIncoming";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 60000;
            await command.ExecuteNonQueryAsync().ConfigAwait();
        }
        finally
        {
            await connection.CloseAsync().ConfigAwait();
        }
    }

    protected virtual async Task Dispose(bool disposing)
    {
        if (disposing)
        {
            await connection.DisposeAsync();
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        this.Dispose(true).Wait();
        GC.SuppressFinalize(this);
    }

    public async Task AddIncomingInventories(IEnumerable<Inventory> inventories)
    {
        await connection.OpenAsync().ConfigAwait();
        try
        {
            using var command = connection.CreateCommand();
            command.CommandText = @"
                        insert into InventoryIncoming (ProductId, StoreId, Quantity)
                        select ProductId, StoreId, Quantity
                        from @inventories
                        except select ProductId, StoreId, Quantity from InventoryIncoming";
            var param = command.Parameters.AddWithValue("@inventories", ToDataTable(inventories));
            param.TypeName = "dbo.IncomingInventory";
            param.SqlDbType = SqlDbType.Structured;
            await command.ExecuteNonQueryAsync().ConfigAwait();
        }
        finally
        {
            await connection.CloseAsync().ConfigAwait();
        }
    }

    private static DataTable ToDataTable(IEnumerable<Inventory> inventories)
    {
        DataTable table = new();
        table.Columns.Add("ProductId", typeof(int));
        table.Columns.Add("StoreId", typeof(int));
        table.Columns.Add("Quantity", typeof(int));
        foreach (Inventory inventory in inventories)
        {
            table.Rows.Add(inventory.ProductId, inventory.StoreId, inventory.Quantity);
        }

        return table;
    }
}
