using Microsoft.Extensions.Logging;
using Spurious2.Core;
using Spurious2.Core.LcboImporting.Domain;
using System.Data;
using System.Data.SqlClient;

namespace Spurious2.SqlRepositories.LcboImporting.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly SqlConnection connection;
    private readonly Func<SqlConnection> connectionFactory;
    private readonly ILogger<StoreRepository> logger;

    public InventoryRepository(SqlConnection connection, Func<SqlConnection> connectionFactory, ILogger<StoreRepository> logger)
    {
        this.connection = connection;
        this.connectionFactory = connectionFactory;
        this.logger = logger;
    }

    public async Task ClearIncomingInventory()
    {
        await this.connection.OpenAsync().ConfigAwait();
        try
        {
            using var command = this.connection.CreateCommand();
            command.CommandText = "DELETE FROM InventoryIncoming";
            command.CommandTimeout = 120000;
            await command.ExecuteNonQueryAsync().ConfigAwait();
            this.logger.LogInformation("Cleared InventoryIncoming table");
        }
        finally
        {
            await this.connection.CloseAsync().ConfigAwait();
        }
    }

    public async Task ClearInventoryPages()
    {
        await this.connection.OpenAsync().ConfigAwait();
        try
        {
            using var command = this.connection.CreateCommand();
            command.CommandText = "DELETE FROM InventoryPage";
            command.CommandTimeout = 120000;
            await command.ExecuteNonQueryAsync().ConfigAwait();
        }
        finally
        {
            await this.connection.CloseAsync().ConfigAwait();
        }
    }

    public async Task UpdateInventoriesFromIncoming()
    {
        await this.connection.OpenAsync().ConfigAwait();
        try
        {
            using var command = this.connection.CreateCommand();
            command.CommandText = "UpdateInventoriesFromIncoming";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 60000;
            await command.ExecuteNonQueryAsync().ConfigAwait();
        }
        finally
        {
            await this.connection.CloseAsync().ConfigAwait();
        }
    }

    protected virtual async Task Dispose(bool disposing)
    {
        if (disposing)
        {
            await this.connection.DisposeAsync();
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
        await this.connection.OpenAsync().ConfigAwait();
        try
        {
            using var command = this.connection.CreateCommand();
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
            await this.connection.CloseAsync().ConfigAwait();
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
