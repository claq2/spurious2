namespace Spurious2.Core2.Stores;

public interface IStoreImportingService
{
    public Task ImportStoresFromCsvFile(string filenameAndPath);
}
