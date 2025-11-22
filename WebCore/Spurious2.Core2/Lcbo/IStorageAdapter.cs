using Azure.Storage.Blobs;

namespace Spurious2.Core2.Lcbo;

public interface IStorageAdapter
{
    public Task<string> GetStoreContents(string storeId);
    public Task<string> GetInventoryContents(string productId);
    public Task<bool> StoreExists(string storeId);
    public Task WriteProductId(string productId);
    public Task WriteInventory(string productId, string pageContent);
    public Task WriteStore(string storeId, string pageContent);
    public Task WriteLastProduct(string input);
    public Task WriteLastInventory(string input);
    public Task ClearStorage(BlobContainerClient? productsClient = null, BlobContainerClient? inventoriesClient = null,
        BlobContainerClient? storesClient = null);
}
