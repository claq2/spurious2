using Microsoft.Extensions.Logging;
using Spurious2.Core;
using Spurious2.Core.LcboImporting.Domain;
using System.Data;
using System.Data.SqlClient;

namespace Spurious2.SqlRepositories.LcboImporting.Repositories;

public class StoreRepository : IStoreRepository
{
    private readonly SqlConnection connection;
    private readonly ILogger<StoreRepository> logger;

    public StoreRepository(SqlConnection connection, ILogger<StoreRepository> logger)
    {
        this.logger = logger;
        this.connection = connection;
    }

    public async Task UpdateStoreVolumes()
    {
        await this.connection.OpenAsync().ConfigAwait();
        try
        {
            using var command = this.connection.CreateCommand();
            command.CommandText = "UpdateStoreVolumes";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 60000;
            await command.ExecuteNonQueryAsync().ConfigAwait();
        }
        finally
        {
            await this.connection.CloseAsync().ConfigAwait();
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

    public async Task ClearIncomingStores()
    {
        await this.connection.OpenAsync().ConfigAwait();
        try
        {
            using var command = this.connection.CreateCommand();
            command.CommandText = "DELETE FROM StoreIncoming";
            await command.ExecuteNonQueryAsync().ConfigAwait();
            this.logger.LogInformation("Cleared StoreIncoming table");
        }
        finally
        {
            await this.connection.CloseAsync().ConfigAwait();
        }
    }

    public async Task UpdateIncomingStore(Store store)
    {
        await this.connection.OpenAsync().ConfigAwait();
        try
        {
            using var command = this.connection.CreateCommand();
            command.CommandText = @"UPDATE [StoreIncoming] SET [City] = @city,
                        [StoreName] = @storename,
                        [Latitude] = @latitude,
                        [Longitude] = @longitude,
                        [StoreDone] = 1
                        WHERE [Id] = @id";
            var idParam = new SqlParameter("@id", store.Id);
            var cityParam = new SqlParameter("@city", store.City);
            var storeNameParam = new SqlParameter("@storename", store.Name);
            var latNameParam = new SqlParameter("@latitude", store.Latitude);
            var longNameParam = new SqlParameter("@longitude", store.Longitude);

            command.Parameters.Add(idParam);
            command.Parameters.Add(cityParam);
            command.Parameters.Add(storeNameParam);
            command.Parameters.Add(latNameParam);
            command.Parameters.Add(longNameParam);
            await command.ExecuteNonQueryAsync().ConfigAwait();
        }
        catch (Exception ex)
        {
            this.logger.LogWarning(ex, "Problem updating incoming store");
            throw;
        }
        finally
        {
            await this.connection.CloseAsync().ConfigAwait();
        }
    }

    public async Task AddIncomingStoreIds(List<int> storeIds)
    {
        await this.connection.OpenAsync().ConfigAwait();
        try
        {
            using var command = this.connection.CreateCommand();
            command.CommandText = @"
                        insert into StoreIncoming (id)
                        select Id
                        from @storeIds
                        where Id not in (select Id from StoreIncoming)";
            var param = command.Parameters.AddWithValue("@storeIds", ToDataTable(storeIds));
            param.TypeName = "dbo.IncomingStore";
            param.SqlDbType = SqlDbType.Structured;
            await command.ExecuteNonQueryAsync().ConfigAwait();
        }
        finally
        {
            await this.connection.CloseAsync().ConfigAwait();
        }
    }

    public async Task UpdateStoresFromIncoming()
    {
        await this.connection.OpenAsync().ConfigAwait();
        try
        {
            // Call sproc to update table and clear incoming table
            using var updateStoresCommand = this.connection.CreateCommand();
            updateStoresCommand.CommandText = "UpdateStoresFromIncoming";
            updateStoresCommand.CommandType = System.Data.CommandType.StoredProcedure;
            updateStoresCommand.CommandTimeout = 60000;
            await updateStoresCommand.ExecuteNonQueryAsync().ConfigAwait();
        }
        finally
        {
            await this.connection.CloseAsync().ConfigAwait();
        }
    }

    private static DataTable ToDataTable(IEnumerable<int> storeIds)
    {
        DataTable table = new();
        table.Columns.Add("Id", typeof(int));
        foreach (var storeId in storeIds)
        {
            table.Rows.Add(storeId);
        }

        return table;
    }
}
