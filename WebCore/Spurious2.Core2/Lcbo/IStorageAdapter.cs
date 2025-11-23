using Azure.Storage.Blobs;

namespace Spurious2.Core2.Lcbo;

public interface IStorageAdapter
{
    public Task<string> GetStoreContents(BlobContainerClient storeBcc, string storeId);
    public Task<string> GetInventoryContents(BlobContainerClient inventoryBcc, string productId);
    public Task<bool> StoreExists(BlobContainerClient storeBcc, string storeId);
    public Task WriteProductId(BlobContainerClient productBcc, string productId);
    public Task WriteInventory(BlobContainerClient inventoryBcc, string productId, string pageContent);
    public Task WriteStore(BlobContainerClient storesBcc, string storeId, string pageContent);
    public Task WriteLastProduct(BlobContainerClient lastProductBcc, string input);
    public Task WriteLastInventory(BlobContainerClient lastInventoryBcc, string input);
    public Task ClearStorage(BlobContainerClient productsClient,
        BlobContainerClient inventoriesClient,
        BlobContainerClient storesClient);
}
