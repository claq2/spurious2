namespace Spurious2.Core2.Lcbo;

public interface IQueueAdapter
{
    public Task ClearQueues();
    public Task WriteProductId(string productId);
    public Task WriteInventoryId(string productId);
    public Task WriteStoreId(string storeId);
}
