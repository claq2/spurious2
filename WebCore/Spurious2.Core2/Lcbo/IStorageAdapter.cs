namespace Spurious2.Core2.Lcbo;

public interface IStorageAdapter
{
    public Task<bool> StoreExists(string storeId);
    public Task WriteProductId(string productId);
    public Task WriteInventory(string productId, string pageContent);
    public Task WriteStore(string storeId, string pageContent);
    public Task WriteLastProduct(string input);
    public Task WriteLastInventory(string input);
    public Task ClearStorage();
}
