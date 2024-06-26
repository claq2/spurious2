using System.Globalization;
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
        logger.ClearedForImporting();
    }

    public async Task SignalLastProductDone()
    {
        await storageAdapter.WriteLastProduct("Done products!").ConfigAwait();
        logger.SignalLastProductDone();
    }

    public async Task GetProductPages(ProductType productType)
    {
        await foreach (var products in lcboAdapter.GetCategorizedProducts(productType).ConfigAwait())
        {
            _ = await spuriousRepository.ImportAFewProducts(products).ConfigAwait();
            foreach (var product in products)
            {
                await storageAdapter.WriteProductId(product.Id.ToString(CultureInfo.InvariantCulture)).ConfigAwait();
            }
        }
    }

    public async Task ProcessProductBlob(string productId)
    {
        var contents = await lcboAdapter.GetAllStoresInventory(productId).ConfigAwait();
        await storageAdapter.WriteInventory(productId, contents).ConfigAwait();
        logger.ProcessedProduct(productId);
    }

    public async Task ProcessInventoryBlob(string productId, Stream inventoryStream)
    {
        // Add store info if blob doesn't exist
        // Mark prod-inv done
        var inventories = await lcboAdapter.ExtractInventoriesAndStoreIds(productId, inventoryStream).ConfigAwait();
        logger.FoundInventoryForProduct(inventories.Count(), productId);
        var storeIds = inventories.Select(i => i.Inventory.StoreId).ToList();
        await spuriousRepository.AddIncomingStoreIds(storeIds).ConfigAwait();
        await spuriousRepository.AddIncomingInventories(inventories.Select(i => i.Inventory).ToList()).ConfigAwait();
        foreach (var (inventory, uri) in inventories)
        {
            if (!await storageAdapter.StoreExists(inventory.StoreId.ToString(CultureInfo.InvariantCulture)).ConfigAwait())
            {
                var storePage = await lcboAdapter.GetStorePage(uri).ConfigAwait();
                await storageAdapter.WriteStore(inventory.StoreId.ToString(CultureInfo.InvariantCulture), storePage).ConfigAwait();
            }
        }

        await spuriousRepository.MarkIncomingProductDone(productId).ConfigAwait();

        logger.ProcessedInventory(productId);
    }

    public async Task ProcessStoreBlob(string storeId, Stream storeStream)
    {
        var store = await lcboAdapter.GetStoreInfo(storeId, storeStream).ConfigAwait();
        // Write store to StoreIncoming, mark as done
        await spuriousRepository.UpdateIncomingStore(store).ConfigAwait();
        logger.ProcessedStore(storeId);
    }

    public async Task ProcessLastProductBlob(string contents)
    {
        // Get volume info and prod IDs, put in DB
        // Get inv contents, write to end prod-inv blob
        // Mark prod done
        await storageAdapter.WriteLastInventory(contents).ConfigAwait();
        logger.ProcessedLastProduct(contents);
    }

    public async Task ProcessLastInventoryBlob(string contents)
    {
        // Add store info if blob doesn't exist
        // Mark prod-inv done
        // Loop checking for all prod and prod-inv and store pages to be done
        // Call EndImporting, or just do it

        // Prods all done => inventories all done => all stores discovered

        await this.EndImporting().ConfigAwait();
        logger.ProcessedLastInventory(contents);
    }

    public Task EndImporting()
    {
        // Do final update
        logger.EndedImporting();
        return Task.CompletedTask;
    }

    public async Task UpdateAll()
    {
        // UpdateStoresFromIncoming
        await spuriousRepository.UpdateStoresFromIncoming().ConfigAwait();
        // UpdateProductsFromIncoming
        await spuriousRepository.UpdateProductsFromIncoming().ConfigAwait();
        // UpdateInventoriesFromIncoming
        await spuriousRepository.UpdateInventoriesFromIncoming().ConfigAwait();
        // UpdateStoreVolumes
        await spuriousRepository.UpdateStoreVolumes().ConfigAwait();
        // UpdateSubdivisionVolumes
        await spuriousRepository.UpdateSubdivisionVolumes().ConfigAwait();
        logger.EndedDbUpdate();
    }
}
