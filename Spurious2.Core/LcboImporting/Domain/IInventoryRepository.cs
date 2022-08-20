namespace Spurious2.Core.LcboImporting.Domain;

public interface IInventoryRepository : IDisposable
{
    Task UpdateInventoriesFromIncoming();
    Task ClearIncomingInventory();
    Task ClearInventoryPages();
    Task AddIncomingInventories(IEnumerable<Inventory> select);
}
