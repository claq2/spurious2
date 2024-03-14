namespace Spurious2.Core2.Lcbo;

public interface IStorageAdapter
{
    Task<bool> StoreExists(string storeId);
    Task WriteProduct(string productId, string pageContent, ProductType productType);
    Task WriteProductId(string productId);
    Task WriteInventory(string productId, string pageContent);
    Task WriteStore(string storeId, string pageContent);
    Task WriteLastProduct(string input);
    Task WriteLastInventory(string input);
    Task ClearStorage();
}
