using Azure.Storage.Blobs;

namespace Spurious2.Core2.Lcbo;

public interface IStorageAdapter
{
    public Task<string> GetStoreContents(BlobContainerClient storeBcc, string storeId);
    public Task<string> GetInventoryContents(BlobContainerClient inventoryBcc, string productId);
    public Task<bool> StoreExists(BlobContainerClient bcc, string storeId);
    public Task WriteProductId(BlobContainerClient bcc, string productId);
    public Task WriteInventory(BlobContainerClient bcc, string productId, string pageContent);
    public Task WriteStore(BlobContainerClient bcc, string storeId, string pageContent);
    public Task WriteLastProduct(BlobContainerClient bcc, string input);
    public Task WriteLastInventory(BlobContainerClient bcc, string input);
    public Task ClearStorage(BlobContainerClient productsClient, BlobContainerClient inventoriesClient,
        BlobContainerClient storesClient);
}
