using Microsoft.Extensions.Logging;

namespace Spurious2.Core2.Lcbo;

public class ImportingService(ISpuriousRepository spuriousRepository, IStorageAdapter storageAdapter,
    ILogger<ImportingService> logger) : IImportingService
{
    public Task EndImporting() => throw new NotImplementedException();
    public Task GetProductPages(ProductType productType) => throw new NotImplementedException();
    public Task ProcessInventoryBlob(string productId, Stream inventoryStream) => throw new NotImplementedException();
    public Task ProcessLastInventoryBlob(string contents) => throw new NotImplementedException();
    public Task ProcessLastProductBlob(string contents) => throw new NotImplementedException();
    public Task ProcessProductBlob(string productId) => throw new NotImplementedException();
    public Task ProcessStoreBlob(string storeId, Stream storeStream) => throw new NotImplementedException();
    public Task SignalLastProductDone() => throw new NotImplementedException();

    public async Task StartImporting()
    {
        // Clear incoming tables
        await spuriousRepository.ClearIncomingStores().ConfigAwait();
        await spuriousRepository.ClearIncomingProducts().ConfigAwait();
        await spuriousRepository.ClearIncomingInventory().ConfigAwait();
        await storageAdapter.ClearStorage().ConfigAwait();
        logger.LogInformation("Cleared for importing");
    }

    public Task UpdateAll() => throw new NotImplementedException();
}
