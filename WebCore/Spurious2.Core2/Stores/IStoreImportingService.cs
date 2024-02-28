namespace Spurious2.Core2.Stores;

public interface IStoreImportingService
{
    IEnumerable<StoreIncoming> ReadStores();
}
