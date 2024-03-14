using Spurious2.Core2.Products;

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
#pragma warning disable CA1002 // Do not expose generic lists
    Task ImportAFewProducts(List<ProductIncoming> products);
#pragma warning restore CA1002 // Do not expose generic lists
}
