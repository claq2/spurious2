namespace Spurious2.Core2.Stores;

public interface IStoreImportingService : IDisposable
{
    IEnumerable<StoreIncoming> ReadStores();
}
