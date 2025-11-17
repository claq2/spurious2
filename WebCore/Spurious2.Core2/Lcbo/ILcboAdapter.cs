using Spurious2.Core2.Inventories;
using Spurious2.Core2.Products;
using Spurious2.Core2.Stores;

namespace Spurious2.Core2.Lcbo;

public interface ILcboAdapter
{
    public StoreIncoming GetStoreInfo(string storeId, string contents);
#pragma warning disable CA1002 // Do not expose generic lists
    public List<(InventoryIncoming Inventory, Uri Uri)> ExtractInventoriesAndStoreIds(string productId, string contents);
#pragma warning restore CA1002 // Do not expose generic lists
    public Task<StoreIncoming> GetStoreInfo(string storeId, Stream storeStream);
    public Task<string> GetStorePage(Uri storeUri);

    /// <summary>
    /// Gets the products for the given product type
    /// </summary>
    /// <returns>IAsyncEnumerable<List<Product2>>></returns>
    public IAsyncEnumerable<IEnumerable<ProductIncoming>> GetCategorizedProducts(ProductType productType);

    public Task<string> GetAllStoresInventory(string productId);
    public Task<IEnumerable<(InventoryIncoming Inventory, Uri Uri)>> ExtractInventoriesAndStoreIds(string productId, Stream inventoryStream);
}
