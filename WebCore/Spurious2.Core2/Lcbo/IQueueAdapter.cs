using Azure.Storage.Queues;

namespace Spurious2.Core2.Lcbo;

public interface IQueueAdapter
{
    public Task ClearQueues(QueueClient productsQueue,
        QueueClient inventoriesQueue,
        QueueClient storesQueue);
    public Task WriteProductId(QueueClient productQc, string productId);
    public Task WriteInventoryId(QueueClient inventoryQc, string productId);
    public Task WriteStoreId(QueueClient storeQc, string storeId);
}
