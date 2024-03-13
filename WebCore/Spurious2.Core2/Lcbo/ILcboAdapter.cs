using Spurious2.Core2.Inventories;
using Spurious2.Core2.Products;
using Spurious2.Core2.Stores;

namespace Spurious2.Core2.Lcbo;

public interface ILcboAdapter
{
    StoreIncoming GetStoreInfo(string storeId, string contents);
#pragma warning disable CA1002 // Do not expose generic lists
    List<(InventoryIncoming Inventory, Uri Uri)> ExtractInventoriesAndStoreIds(string productId, string contents);
#pragma warning restore CA1002 // Do not expose generic lists
    Task<StoreIncoming> GetStoreInfo(string storeId, Stream storeStream);
    Task<string> GetStorePage(Uri storeUri);
    //IAsyncEnumerable<List<Product2>> GetAllProducts();

    /// <summary>
    /// Gets the products for the given product type
    /// </summary>
    /// <returns>IAsyncEnumerable<List<Product2>>></returns>
    IAsyncEnumerable<List<ProductIncoming>> GetCategorizedProducts(ProductType productType);

    Task<string> GetAllStoresInventory(string productId);
    Task<List<(InventoryIncoming Inventory, Uri Uri)>> ExtractInventoriesAndStoreIds(string productId, Stream inventoryStream);
}
