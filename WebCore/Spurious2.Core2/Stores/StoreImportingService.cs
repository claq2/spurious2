using CsvHelper;

namespace Spurious2.Core2.Stores;

public class StoreImportingService(ISpuriousRepository spuriousRepository) : IStoreImportingService
{
    public async Task ImportStoresFromCsvFile(string filenameAndPath, CancellationToken cancellationToken)
    {
        using var storeReader = CsvReader.FromFile(filenameAndPath);
        var stores = storeReader.GetRecordsAsync<StoreIncoming>(cancellationToken);
        await spuriousRepository.ImportStoresFromCsv(stores, cancellationToken).ConfigAwait();
    }
}
