using Spurious2.Core.LcboImporting.Domain;

namespace Spurious2.Core.LcboImporting.Adapters;

public interface ILcboAdapterPaged2
{
    Task<Store> GetStoreInfo(string storeId, Stream storeStream);
    Task<string> GetStorePage(Uri storeUri);
    //IAsyncEnumerable<List<Product2>> GetAllProducts();

    /// <summary>
    /// Gets the products for the given product type
    /// </summary>
    /// <returns>IAsyncEnumerable<List<Product2>>></returns>
    IAsyncEnumerable<List<Product>> GetCategorizedProducts(ProductType productType);

    Task<string> GetAllStoresInventory(string productId);
    Task<List<(Inventory Inventory, Uri Uri)>> ExtractInventoriesAndStoreIds(string productId, Stream inventoryStream);
}
