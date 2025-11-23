using Azure.Storage.Blobs;
using Azure.Storage.Queues;

namespace Spurious2.Core2.Lcbo;

public interface IImportingService
{
    public IAsyncEnumerable<string> GetProductPagesAndReturnIds(ProductType productType);
    public Task ProcessStoreBlob(BlobContainerClient storeBcc, string storeId);
    public Task ProcessInventoryBlob(BlobContainerClient invBcc,
        BlobContainerClient storeBcc,
        QueueClient storesQueueClient,
        string productId);
    //public Task ProcessStoreBlob(string storeId, Stream storeStream);
    public Task SignalLastProductDone(BlobContainerClient bcc);
    public Task StartImporting(BlobContainerClient productsClient,
        BlobContainerClient inventoriesClient,
        BlobContainerClient storesClient,
        QueueClient productsQueue,
        QueueClient inventoriesQueue,
        QueueClient storesQueue);
    //public Task ProcessInventoryBlob(BlobContainerClient storeBcc, string productId, Stream inventoryStream);
    public Task ProcessProductBlob(BlobContainerClient inventoryBcc, QueueClient inventoryQc, string productId);
    public Task ProcessLastProductBlob(BlobContainerClient bcc, string contents);
    public Task ProcessLastInventoryBlob(string contents);
    public Task EndImporting();
    public Task GetProductPages(QueueClient qc, ProductType productType);
    public Task UpdateAll();
}
