namespace Spurious2.Core.LcboImporting.Domain;

public interface IStoreRepository : IDisposable
{
    Task UpdateIncomingStore(Store store);
    Task UpdateStoreVolumes();
    Task ClearIncomingStores();
    Task AddIncomingStoreIds(List<int> storeIds);
    Task UpdateStoresFromIncoming();
}
