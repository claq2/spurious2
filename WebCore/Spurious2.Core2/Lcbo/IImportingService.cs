using Azure.Storage.Blobs;
using Azure.Storage.Queues;

namespace Spurious2.Core2.Lcbo;

public interface IImportingService
{
    public Task<bool> AreAnyIncomingRecordsNotDone(CancellationToken cancellationToken);
    public IAsyncEnumerable<string> GetProductPagesAndReturnIds(ProductType productType, CancellationToken cancellationToken);
    public Task ProcessStoreBlob(BlobContainerClient storeBcc, string storeId, CancellationToken cancellationToken);
    public Task ProcessInventoryBlob(BlobContainerClient invBcc,
        BlobContainerClient storeBcc,
        QueueClient storesQueueClient,
        string productId,
        CancellationToken cancellationToken);
    //public Task ProcessStoreBlob(string storeId, Stream storeStream);
    public Task SignalLastProductDone(BlobContainerClient bcc, CancellationToken cancellationToken);
    public Task StartImporting(BlobContainerClient productsClient,
        BlobContainerClient inventoriesClient,
        BlobContainerClient storesClient,
        QueueClient productsQueue,
        QueueClient inventoriesQueue,
        QueueClient storesQueue,
        CancellationToken cancellationToken);
    //public Task ProcessInventoryBlob(BlobContainerClient storeBcc, string productId, Stream inventoryStream);
    public Task ProcessProductBlob(BlobContainerClient inventoryBcc, QueueClient inventoryQc, string productId, CancellationToken cancellationToken);
    public Task ProcessLastProductBlob(BlobContainerClient bcc, string contents, CancellationToken cancellationToken);
    public Task ProcessLastInventoryBlob(string contents, CancellationToken cancellationToken);
    public Task EndImporting();
    public Task GetProductPages(QueueClient qc, ProductType productType, CancellationToken cancellationToken);
    public Task UpdateAll(CancellationToken cancellationToken);
}
