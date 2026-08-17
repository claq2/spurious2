using System.Globalization;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.Extensions.Logging;

namespace Spurious2.Core2.Lcbo;

public class ImportingService(ISpuriousRepository spuriousRepository,
    IStorageAdapter storageAdapter,
    IQueueAdapter queueAdapter,
    ILcboAdapter lcboAdapter,
    ILogger<ImportingService> logger) : IImportingService
{
    public async Task StartImporting(BlobContainerClient productsClient,
        BlobContainerClient inventoriesClient,
        BlobContainerClient storesClient,
        QueueClient productsQueue,
        QueueClient inventoriesQueue,
        QueueClient storesQueue)
    {
        // Clear incoming tables
        await spuriousRepository.ClearIncomingStores().ConfigAwait();
        await spuriousRepository.ClearIncomingProducts().ConfigAwait();
        await spuriousRepository.ClearIncomingInventory().ConfigAwait();
        await storageAdapter.ClearStorage(productsClient, inventoriesClient, storesClient).ConfigAwait();
        await queueAdapter.ClearQueues(productsQueue, inventoriesQueue, storesQueue).ConfigAwait();
        logger.ClearedForImporting();
    }

    public async Task SignalLastProductDone(BlobContainerClient bcc)
    {
        await storageAdapter.WriteLastProduct(bcc, "Done products!").ConfigAwait();
        logger.SignalLastProductDone();
    }

    public async Task GetProductPages(QueueClient qc, ProductType productType)
    {
        await foreach (var products in lcboAdapter.GetCategorizedProducts(productType).ConfigAwait())
        {
            _ = await spuriousRepository.ImportAFewProducts(products).ConfigAwait();
            foreach (var product in products)
            {
                //await storageAdapter.WriteProductId(product.Id.ToString(CultureInfo.InvariantCulture)).ConfigAwait();
                await queueAdapter.WriteProductId(qc, product.Id.ToString(CultureInfo.InvariantCulture)).ConfigAwait();
            }
        }
    }

    public async IAsyncEnumerable<string> GetProductPagesAndReturnIds(ProductType productType)
    {
        await foreach (var products in lcboAdapter.GetCategorizedProducts(productType).ConfigAwait())
        {
            _ = await spuriousRepository.ImportAFewProducts(products).ConfigAwait();
            foreach (var product in products)
            {
                yield return product.Id.ToString(CultureInfo.InvariantCulture);
                //await storageAdapter.WriteProductId(product.Id.ToString(CultureInfo.InvariantCulture)).ConfigAwait();
                //await queueAdapter.WriteProductId(product.Id.ToString(CultureInfo.InvariantCulture)).ConfigAwait();
            }
        }
    }

    public async Task ProcessProductBlob(BlobContainerClient inventoryBcc, QueueClient inventoryQc, string productId)
    {
        var contents = await lcboAdapter.GetAllStoresInventory(productId).ConfigAwait();
        await storageAdapter.WriteInventory(inventoryBcc, productId, contents).ConfigAwait();
        await queueAdapter.WriteInventoryId(inventoryQc, productId).ConfigAwait();
        logger.ProcessedProduct(productId);
    }

    //public async Task ProcessInventoryBlob(BlobContainerClient storeBcc, string productId, Stream inventoryStream)
    //{
    //    // Add store info if blob doesn't exist
    //    // Mark prod-inv done
    //    var inventories = await lcboAdapter.ExtractInventoriesAndStoreIds(productId, inventoryStream).ConfigAwait();
    //    logger.FoundInventoryForProduct(inventories.Count(), productId);
    //    var storeIds = inventories.Select(i => i.Inventory.StoreId).ToList();
    //    await spuriousRepository.AddIncomingStoreIds(storeIds).ConfigAwait();
    //    await spuriousRepository.AddIncomingInventories(inventories.Select(i => i.Inventory).ToList()).ConfigAwait();
    //    foreach (var (inventory, uri) in inventories)
    //    {
    //        if (!await storageAdapter.StoreExists(storeBcc, inventory.StoreId.ToString(CultureInfo.InvariantCulture)).ConfigAwait())
    //        {
    //            var storePage = await lcboAdapter.GetStorePage(uri).ConfigAwait();
    //            await storageAdapter.WriteStore(storeBcc, inventory.StoreId.ToString(CultureInfo.InvariantCulture), storePage).ConfigAwait();
    //        }
    //    }

    //    await spuriousRepository.MarkIncomingProductDone(productId).ConfigAwait();

    //    logger.ProcessedInventory(productId);
    //}

    public async Task ProcessInventoryBlob(BlobContainerClient invBcc,
        BlobContainerClient storeBcc,
        QueueClient storesQueueClient,
        string productId)
    {
        // Add store info if blob doesn't exist
        // Mark prod-inv done
        var inventoryContents = await storageAdapter.GetInventoryContents(invBcc, productId).ConfigAwait();
        var inventories = lcboAdapter.ExtractInventoriesAndStoreIds(productId, inventoryContents);
        logger.FoundInventoryForProduct(inventories.Count, productId);
        var storeIds = inventories.Select(i => i.Inventory.StoreId).ToList();
        //var storeIdsToBeAdded = await spuriousRepository.GetStoresToBeAdded(storeIds).ConfigAwait();
        var storeIdsToAdd = await spuriousRepository.AddIncomingStoreIdsAndReturnAddedIds(storeIds).ConfigAwait();
        await spuriousRepository.AddIncomingInventories(inventories.Select(i => i.Inventory).ToList()).ConfigAwait();
        var storeIdToStoreUrlMap = inventories.ToDictionary(i => i.Inventory.StoreId, i => i.Uri);
        foreach (var storeId in storeIdsToAdd)
        {
            var storePage = await lcboAdapter.GetStorePage(storeIdToStoreUrlMap[storeId]).ConfigAwait();
            await storageAdapter.WriteStore(storeBcc, storeId.ToString(CultureInfo.InvariantCulture), storePage).ConfigAwait();
            await queueAdapter.WriteStoreId(storesQueueClient, storeId.ToString(CultureInfo.InvariantCulture)).ConfigAwait();
        }
        //foreach (var (inventory, uri) in inventories)
        //{
        //    if (storeIdsToAdd.Contains(inventory.StoreId))
        //    //if (!await storageAdapter.StoreExists(inventory.StoreId.ToString(CultureInfo.InvariantCulture)).ConfigAwait())
        //    {
        //        var storePage = await lcboAdapter.GetStorePage(uri).ConfigAwait();
        //        await storageAdapter.WriteStore(storeBcc, inventory.StoreId.ToString(CultureInfo.InvariantCulture), storePage).ConfigAwait();
        //        await queueAdapter.WriteStoreId(storesQueueClient, inventory.StoreId.ToString(CultureInfo.InvariantCulture)).ConfigAwait();
        //    }
        //}

        await spuriousRepository.MarkIncomingProductDone(productId).ConfigAwait();

        logger.ProcessedInventory(productId);
    }

    //public async Task ProcessStoreBlob(string storeId, Stream storeStream)
    //{
    //    var store = await lcboAdapter.GetStoreInfo(storeId, storeStream).ConfigAwait();
    //    // Write store to StoreIncoming, mark as done
    //    await spuriousRepository.UpdateIncomingStore(store).ConfigAwait();
    //    logger.ProcessedStore(storeId);
    //}

    public async Task ProcessStoreBlob(BlobContainerClient storeBcc, string storeId)
    {
        var storeContents = await storageAdapter.GetStoreContents(storeBcc, storeId).ConfigAwait();
        var store = lcboAdapter.GetStoreInfo(storeId, storeContents);
        // Write store to StoreIncoming, mark as done
        await spuriousRepository.UpdateIncomingStore(store).ConfigAwait();
        logger.ProcessedStore(storeId);
    }

    public async Task ProcessLastProductBlob(BlobContainerClient bcc, string contents)
    {
        // Get volume info and prod IDs, put in DB
        // Get inv contents, write to end prod-inv blob
        // Mark prod done
        await storageAdapter.WriteLastInventory(bcc, contents).ConfigAwait();
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

    public async Task<bool> AreAnyIncomingRecordsNotDone()
    {
        return await spuriousRepository.AreAnyIncomingRecordsNotDone().ConfigAwait();
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
