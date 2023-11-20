using Microsoft.Extensions.Logging;
using Spurious2.Core.LcboImporting.Adapters;
using Spurious2.Core.LcboImporting.Domain;
using Spurious2.Core.SubdivisionImporting.Domain;

namespace Spurious2.Core.LcboImporting.Services;

public class ImportingService(
    IStoreRepository storeRepository,
    ILcboAdapterPaged2 lcboAdapter,
    IProductRepository productRepository,
    IInventoryRepository inventoryRepository,
    ISubdivisionRepository subdivisionRepository,
    IStorageAdapter storageAdapter,
    ILogger<ImportingService> logger) : IImportingService
{
    public async Task StartImporting()
    {
        // Clear incoming tables
        await storeRepository.ClearIncomingStores().ConfigAwait();
        await productRepository.ClearIncomingProducts().ConfigAwait();
        await inventoryRepository.ClearIncomingInventory().ConfigAwait();
        await storageAdapter.ClearStorage();
    }

    public async Task SignalLastProductDone()
    {
        await storageAdapter.WriteLastProduct("Done products!");
    }

    public async Task GetProductPages(ProductType productType)
    {
        await foreach (var products in lcboAdapter.GetCategorizedProducts(productType).ConfigAwait())
        {
            await productRepository.ImportAFew(products).ConfigAwait();
            foreach (Product product2 in products)
            {
                await storageAdapter.WriteProductId(product2.Id.ToString());
            }
        }
    }

    public async Task ProcessProductBlob(string productId)
    {
        var contents = await lcboAdapter.GetAllStoresInventory(productId).ConfigAwait();
        await storageAdapter.WriteInventory(productId, contents).ConfigAwait();
        logger.LogInformation("Processed product {productId}", productId);
    }

    public async Task ProcessInventoryBlob(string productId, Stream inventoryStream)
    {
        // Add store info if blob doesn't exist
        // Mark prod-inv done
        var inventories = await lcboAdapter.ExtractInventoriesAndStoreIds(productId, inventoryStream).ConfigAwait();
        logger.LogInformation("Found {count} inventory items for product {productId}",
            inventories.Count,
            productId);
        var storeIds = inventories.Select(i => i.Item1.StoreId).ToList();
        await storeRepository.AddIncomingStoreIds(storeIds).ConfigAwait();
        await inventoryRepository.AddIncomingInventories(inventories.Select(i => i.Item1));
        foreach (var (inventory, uri) in inventories)
        {
            if (!await storageAdapter.StoreExists(inventory.StoreId.ToString()).ConfigAwait())
            {
                var storePage = await lcboAdapter.GetStorePage(uri).ConfigAwait();
                // _ = this.storageAdapter.WriteStore(inventory.StoreId.ToString(), storePage).ConfigAwait();
                await storageAdapter.WriteStore(inventory.StoreId.ToString(), storePage).ConfigAwait();
            }
        }

        await productRepository.MarkIncomingProductDone(productId).ConfigAwait();

        logger.LogInformation("Processed inventory {productId}", productId);
    }

    public async Task ProcessStoreBlob(string storeId, Stream storeStream)
    {
        var store = await lcboAdapter.GetStoreInfo(storeId, storeStream);
        // Write store to StoreIncoming, mark as done
        await storeRepository.UpdateIncomingStore(store).ConfigAwait();
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

    public async Task UpdateAll()
    {
        // UpdateStoresFromIncoming
        await storeRepository.UpdateStoresFromIncoming().ConfigAwait();
        // UpdateProductsFromIncoming
        await productRepository.UpdateProductsFromIncoming().ConfigAwait();
        // UpdateInventoriesFromIncoming
        await inventoryRepository.UpdateInventoriesFromIncoming().ConfigAwait();
        // UpdateStoreVolumes
        await storeRepository.UpdateStoreVolumes().ConfigAwait();
        // UpdateSubdivisionVolumes
        await subdivisionRepository.UpdateSubdivisionVolumes().ConfigAwait();
        logger.LogInformation("Ended DB update");
    }
}
