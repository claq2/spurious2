using Azure.Storage.Queues;

namespace Spurious2.Core2.Lcbo;

public interface IQueueAdapter
{
    public Task ClearQueues(QueueClient? productsQueue = null,
        QueueClient? inventoriesQueue = null,
        QueueClient? storesQueue = null);
    public Task WriteProductId(string productId);
    public Task WriteInventoryId(string productId);
    public Task WriteStoreId(string storeId);
}
