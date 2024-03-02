namespace Spurious2.Core2.Stores;

public interface IStoreImportingService : IDisposable
{
    Task ImportStoresFromCsvFile(string filenameAndPath);
}
