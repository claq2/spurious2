using Microsoft.Extensions.Logging;

namespace Spurious2.Core2.Lcbo;

public class ImportingService(ISpuriousRepository spuriousRepository,
    IStorageAdapter storageAdapter,
    ILcboAdapter lcboAdapter,
    ILogger<ImportingService> logger) : IImportingService
{

    public async Task StartImporting()
    {
        // Clear incoming tables
        await spuriousRepository.ClearIncomingStores().ConfigAwait();
        await spuriousRepository.ClearIncomingProducts().ConfigAwait();
        await spuriousRepository.ClearIncomingInventory().ConfigAwait();
        await storageAdapter.ClearStorage().ConfigAwait();
        logger.LogInformation("Cleared for importing");
    }

    public async Task SignalLastProductDone()
    {
        await storageAdapter.WriteLastProduct("Done products!").ConfigAwait();
        logger.LogInformation($"{nameof(SignalLastProductDone)}");
    }

    public Task GetProductPages(ProductType productType) => throw new NotImplementedException();

    public async Task ProcessProductBlob(string productId)
    {
        var contents = await lcboAdapter.GetAllStoresInventory(productId).ConfigAwait();
        await storageAdapter.WriteInventory(productId, contents).ConfigAwait();
        logger.LogInformation("Processed product {productId}", productId);
    }

    public Task ProcessInventoryBlob(string productId, Stream inventoryStream) => throw new NotImplementedException();

    public async Task ProcessStoreBlob(string storeId, Stream storeStream)
    {
        var store = await lcboAdapter.GetStoreInfo(storeId, storeStream).ConfigAwait();
        // Write store to StoreIncoming, mark as done
        await spuriousRepository.UpdateIncomingStore(store).ConfigAwait();
        logger.LogInformation("Processed store {storeId}", storeId);
    }

    public async Task ProcessLastProductBlob(string contents)
    {
        // Get volume info and prod IDs, put in DB
        // Get inv contents, write to end prod-inv blob
        // Mark prod done
        await storageAdapter.WriteLastInventory(contents).ConfigAwait();
        logger.LogInformation("Processed last product {contents}", contents);
    }

    public async Task ProcessLastInventoryBlob(string contents)
    {
        // Add store info if blob doesn't exist
        // Mark prod-inv done
        // Loop checking for all prod and prod-inv and store pages to be done
        // Call EndImporting, or just do it

        // Prods all done => inventories all done => all stores discovered

        await this.EndImporting().ConfigAwait();
        logger.LogInformation("Processed last inventory {contents}", contents);
    }

    public Task EndImporting()
    {
        // Do final update
        logger.LogInformation("Ended importing, doing DB update (no, not really :)");
        return Task.CompletedTask;
    }

    public Task UpdateAll() => throw new NotImplementedException();
}
