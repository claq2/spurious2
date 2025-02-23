namespace Spurious2.Core2.Stores;

public interface IStoreImportingService : IDisposable
{
    public Task ImportStoresFromCsvFile(string filenameAndPath);
}
