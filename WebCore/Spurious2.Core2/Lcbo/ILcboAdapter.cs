using Spurious2.Core2.Inventories;
using Spurious2.Core2.Products;
using Spurious2.Core2.Stores;

namespace Spurious2.Core2.Lcbo;

public interface ILcboAdapter
{
    StoreIncoming GetStoreInfo(string storeId, string contents);
    IEnumerable<(InventoryIncoming Inventory, Uri Uri)> ExtractInventoriesAndStoreIds(string productId, string contents);
    Task<StoreIncoming> GetStoreInfo(string storeId, Stream storeStream);
    Task<string> GetStorePage(Uri storeUri);

    /// <summary>
    /// Gets the products for the given product type
    /// </summary>
    /// <returns>IAsyncEnumerable<List<Product2>>></returns>
    IAsyncEnumerable<IEnumerable<ProductIncoming>> GetCategorizedProducts(ProductType productType);

    Task<string> GetAllStoresInventory(string productId);
    Task<IEnumerable<(InventoryIncoming Inventory, Uri Uri)>> ExtractInventoriesAndStoreIds(string productId, Stream inventoryStream);
}
