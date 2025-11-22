using Azure.Storage.Queues;

namespace Spurious2.Core2.Lcbo;

public interface IQueueAdapter
{
    public Task ClearQueues(QueueClient productsQueue,
        QueueClient inventoriesQueue,
        QueueClient storesQueue);
    public Task WriteProductId(QueueClient bcc, string productId);
    public Task WriteInventoryId(QueueClient bcc, string productId);
    public Task WriteStoreId(QueueClient bcc, string storeId);
}
