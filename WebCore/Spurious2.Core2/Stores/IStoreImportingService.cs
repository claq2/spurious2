namespace Spurious2.Core2.Stores;

public interface IStoreImportingService : IDisposable
{
    Task<IEnumerable<StoreIncoming>> ImportStoresFromCsvFile(string filenameAndPath);
}
