using Azure.Storage.Blobs;
using Azure.Storage.Queues;

namespace Spurious2.Core2.Lcbo;

public interface IImportingService
{
    public IAsyncEnumerable<string> GetProductPagesAndReturnIds(ProductType productType);
    public Task ProcessStoreBlob(string storeId);
    public Task ProcessInventoryBlob(string productId);
    public Task ProcessStoreBlob(string storeId, Stream storeStream);
    public Task SignalLastProductDone();
    public Task StartImporting(BlobContainerClient? productsClient = null,
        BlobContainerClient? inventoriesClient = null,
        BlobContainerClient? storesClient = null,
        QueueClient? productsQueue = null,
        QueueClient? inventoriesQueue = null,
        QueueClient? storesQueue = null);
    public Task ProcessInventoryBlob(string productId, Stream inventoryStream);
    public Task ProcessProductBlob(string productId);
    public Task ProcessLastProductBlob(string contents);
    public Task ProcessLastInventoryBlob(string contents);
    public Task EndImporting();
    public Task GetProductPages(ProductType productType);
    public Task UpdateAll();
}
