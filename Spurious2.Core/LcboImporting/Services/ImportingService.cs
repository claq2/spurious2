using Microsoft.Extensions.Logging;
using Spurious2.Core.LcboImporting.Adapters;
using Spurious2.Core.LcboImporting.Domain;
using Spurious2.Core.SubdivisionImporting.Domain;

namespace Spurious2.Core.LcboImporting.Services;

public class ImportingService : IImportingService
{
    private readonly IStoreRepository storeRepository;
    private readonly IProductRepository productRepository;
    private readonly IInventoryRepository inventoryRepository;
    private readonly ISubdivisionRepository subdivisionRepository;
    private readonly ILcboAdapterPaged2 lcboAdapter;
    private readonly IStorageAdapter storageAdapter;
    private readonly ILogger<ImportingService> logger;

    public ImportingService(
        IStoreRepository storeRepository,
        ILcboAdapterPaged2 lcboAdapter,
        IProductRepository productRepository,
        IInventoryRepository inventoryRepository,
        ISubdivisionRepository subdivisionRepository,
        IStorageAdapter storageAdapter,
        ILogger<ImportingService> logger)
    {
        this.storageAdapter = storageAdapter;
        this.storeRepository = storeRepository;
        this.lcboAdapter = lcboAdapter;
        this.productRepository = productRepository;
        this.inventoryRepository = inventoryRepository;
        this.subdivisionRepository = subdivisionRepository;
        this.logger = logger;
    }

    public async Task StartImporting()
    {
        // Clear incoming tables
        await this.storeRepository.ClearIncomingStores().ConfigAwait();
        await this.productRepository.ClearIncomingProducts().ConfigAwait();
        await this.inventoryRepository.ClearIncomingInventory().ConfigAwait();
        await this.storageAdapter.ClearStorage();
    }

    public async Task SignalLastProductDone()
    {
        await this.storageAdapter.WriteLastProduct("Done products!");
    }

    public async Task GetProductPages(ProductType productType)
    {
        await foreach (var products in this.lcboAdapter.GetCategorizedProducts(productType).ConfigAwait())
        {
            await this.productRepository.ImportAFew(products).ConfigAwait();
            foreach (Product product2 in products)
            {
                await this.storageAdapter.WriteProductId(product2.Id.ToString());
            }
        }
    }

    public async Task ProcessProductBlob(string productId)
    {
        var contents = await this.lcboAdapter.GetAllStoresInventory(productId).ConfigAwait();
        await this.storageAdapter.WriteInventory(productId, contents).ConfigAwait();
        this.logger.LogInformation("Processed product {productId}", productId);
    }

    public async Task ProcessInventoryBlob(string productId, Stream inventoryStream)
    {
        // Add store info if blob doesn't exist
        // Mark prod-inv done
        var inventories = await this.lcboAdapter.ExtractInventoriesAndStoreIds(productId, inventoryStream).ConfigAwait();
        this.logger.LogInformation("Found {count} inventory items for product {productId}",
            inventories.Count,
            productId);
        var storeIds = inventories.Select(i => i.Item1.StoreId).ToList();
        await this.storeRepository.AddIncomingStoreIds(storeIds).ConfigAwait();
        await this.inventoryRepository.AddIncomingInventories(inventories.Select(i => i.Item1));
        foreach (var (inventory, uri) in inventories)
        {
            if (!await this.storageAdapter.StoreExists(inventory.StoreId.ToString()).ConfigAwait())
            {
                var storePage = await this.lcboAdapter.GetStorePage(uri).ConfigAwait();
                // _ = this.storageAdapter.WriteStore(inventory.StoreId.ToString(), storePage).ConfigAwait();
                await this.storageAdapter.WriteStore(inventory.StoreId.ToString(), storePage).ConfigAwait();
            }
        }

        await this.productRepository.MarkIncomingProductDone(productId).ConfigAwait();

        this.logger.LogInformation("Processed inventory {productId}", productId);
    }

    public async Task ProcessStoreBlob(string storeId, Stream storeStream)
    {
        var store = await this.lcboAdapter.GetStoreInfo(storeId, storeStream);
        // Write store to StoreIncoming, mark as done
        await this.storeRepository.UpdateIncomingStore(store).ConfigAwait();
        this.logger.LogInformation("Processed store {storeId}", storeId);
    }

    public async Task ProcessLastProductBlob(string contents)
    {
        // Get volume info and prod IDs, put in DB
        // Get inv contents, write to end prod-inv blob
        // Mark prod done
        await this.storageAdapter.WriteLastInventory(contents).ConfigAwait();
        this.logger.LogInformation("Processed last product {contents}", contents);
    }

    public async Task ProcessLastInventoryBlob(string contents)
    {
        // Add store info if blob doesn't exist
        // Mark prod-inv done
        // Loop checking for all prod and prod-inv and store pages to be done
        // Call EndImporting, or just do it

        // Prods all done => inventories all done => all stores discovered

        await this.EndImporting().ConfigAwait();
        this.logger.LogInformation("Processed last inventory {contents}", contents);
    }

    public Task EndImporting()
    {
        // Do final update
        this.logger.LogInformation("Ended importing, doing DB update (no, not really :)");
        return Task.CompletedTask;
    }

    public async Task UpdateAll()
    {
        // UpdateStoresFromIncoming
        await this.storeRepository.UpdateStoresFromIncoming().ConfigAwait();
        // UpdateProductsFromIncoming
        await this.productRepository.UpdateProductsFromIncoming().ConfigAwait();
        // UpdateInventoriesFromIncoming
        await this.inventoryRepository.UpdateInventoriesFromIncoming().ConfigAwait();
        // UpdateStoreVolumes
        await this.storeRepository.UpdateStoreVolumes().ConfigAwait();
        // UpdateSubdivisionVolumes
        await this.subdivisionRepository.UpdateSubdivisionVolumes().ConfigAwait();
        this.logger.LogInformation("Ended DB update");
    }
}
