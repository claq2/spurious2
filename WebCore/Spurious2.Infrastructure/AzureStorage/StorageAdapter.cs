using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using Spurious2.Core2;
using Spurious2.Core2.Lcbo;

namespace Spurious2.Infrastructure.AzureStorage;

public class StorageAdapter(
    //Func<string, BlobContainerClient> clientFactory,
    ILogger<StorageAdapter> logger) : IStorageAdapter
{
    public async Task ClearStorage(BlobContainerClient productsClient,
        BlobContainerClient inventoriesClient,
        BlobContainerClient storesClient)
    {
        ArgumentNullException.ThrowIfNull(productsClient);
        ArgumentNullException.ThrowIfNull(inventoriesClient);
        ArgumentNullException.ThrowIfNull(storesClient);
        //productsClient ??= clientFactory.Invoke("products");
        //inventoriesClient ??= clientFactory.Invoke("inventories");
        //storesClient ??= clientFactory.Invoke("stores");
        //var lastProductClient = clientFactory.Invoke("last-product");
        //var lastInventoryClient = clientFactory.Invoke("last-inventory");
        logger.DeletingContainers();
        await Task.WhenAll(
        [
            productsClient.DeleteIfExistsAsync(),
            inventoriesClient.DeleteIfExistsAsync(),
            storesClient.DeleteIfExistsAsync(),
            //lastProductClient.DeleteIfExistsAsync(),
            //lastInventoryClient.DeleteIfExistsAsync()
        ]).ConfigAwait();

        logger.DeletedContainers();

        for (var i = 0; i < 3; i++)
        {
            logger.CreatingContainers(i + 1);

            var errors = false;
            try
            {
                await productsClient.CreateIfNotExistsAsync().ConfigAwait();
                logger.CreatedProducts();
            }
            catch (Exception ex)
            {
                logger.CouldNotCreateProducts(ex);
                errors = true;
            }

            try
            {
                await inventoriesClient.CreateIfNotExistsAsync().ConfigAwait();
                logger.CreatedInventories();
            }
            catch (Exception ex)
            {
                logger.CouldNotCreateInventories(ex);
                errors = true;
            }

            try
            {
                await storesClient.CreateIfNotExistsAsync().ConfigAwait();
                logger.CreatedStores();
            }
            catch (Exception ex)
            {
                logger.CouldNotCreateStores(ex);
                errors = true;
            }

            //try
            //{
            //    await lastProductClient.CreateIfNotExistsAsync().ConfigAwait();
            //    logger.CreatedLastProduct();
            //}
            //catch (Exception ex)
            //{
            //    logger.CouldNotCreateLastProduct(ex);
            //    errors = true;
            //}

            //try
            //{
            //    await lastInventoryClient.CreateIfNotExistsAsync().ConfigAwait();
            //    logger.CreatedLastInventory();
            //}
            //catch (Exception ex)
            //{
            //    logger.CouldNotCreateLastInventory(ex);
            //    errors = true;
            //}

            if (i == 2 && errors)
            {
                logger.FailedToCreateContainer();
                throw new InvalidOperationException();
            }

            if (errors)
            {
                await Task.Delay(TimeSpan.FromSeconds(30)).ConfigAwait();
            }
            else
            {
                break;
            }
        }
    }

    public async Task WriteProductId(BlobContainerClient productBcc, string productId)
    {
        //var bcc = clientFactory.Invoke("products");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(productId);
        ArgumentNullException.ThrowIfNull(productBcc);
        var bc = productBcc.GetBlobClient(productId);
        if (!await bc.ExistsAsync().ConfigAwait())
        {
            var ec = BlobErrorCode.BlobAlreadyExists.ToString();
            try
            {
                await bc.UploadTextAsync(productId).ConfigAwait();
            }
            catch (RequestFailedException ex) when (ex.ErrorCode == ec)
            {
                // Ignore only if it's not a concurrency issue
            }
        }
    }

    public async Task WriteInventory(BlobContainerClient inventoryBcc, string productId, string pageContent)
    {
        //var bcc = clientFactory.Invoke("inventories");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(productId);
        ArgumentNullException.ThrowIfNull(inventoryBcc);
        var bc = inventoryBcc.GetBlobClient(productId);
        if (!await bc.ExistsAsync().ConfigAwait())
        {
            var ec = BlobErrorCode.BlobAlreadyExists.ToString();
            try
            {
                await bc.UploadTextAsync(pageContent).ConfigAwait();
            }
            catch (RequestFailedException ex) when (ex.ErrorCode == ec)
            {
                // Ignore only if it's not a concurrency issue
            }
        }
    }

    public async Task<bool> StoreExists(BlobContainerClient storeBcc, string storeId)
    {
        //var bcc = clientFactory.Invoke("stores");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(storeId);
        ArgumentNullException.ThrowIfNull(storeBcc);
        var bc = storeBcc.GetBlobClient(storeId);
        return await bc.ExistsAsync().ConfigAwait();
    }

    public async Task WriteStore(BlobContainerClient storesBcc, string storeId, string pageContent)
    {
        //var bcc = clientFactory.Invoke("stores");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(storeId);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(pageContent);
        ArgumentNullException.ThrowIfNull(storesBcc);
        var bc = storesBcc.GetBlobClient(storeId);
        if (!await bc.ExistsAsync().ConfigAwait())
        {
            var ec = BlobErrorCode.BlobAlreadyExists.ToString();
            try
            {
                await bc.UploadTextAsync(pageContent).ConfigAwait();
            }
            catch (RequestFailedException ex) when (ex.ErrorCode == ec)
            {
                // Ignore only if it's not a concurrency issue
            }
        }
    }

    public async Task WriteLastInventory(BlobContainerClient lastInventoryBcc, string input)
    {
        //var bcc = clientFactory.Invoke("last-inventory");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(input);
        ArgumentNullException.ThrowIfNull(lastInventoryBcc);
        var bc = lastInventoryBcc.GetBlobClient(Guid.NewGuid().ToString());
        await bc.UploadTextAsync(input).ConfigAwait();
    }

    public async Task WriteLastProduct(BlobContainerClient lastProductBcc, string input)
    {
        //var bcc = clientFactory.Invoke("last-product");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(input);
        ArgumentNullException.ThrowIfNull(lastProductBcc);
        var bc = lastProductBcc.GetBlobClient(Guid.NewGuid().ToString());
        await bc.UploadTextAsync(input).ConfigAwait();
    }

    public async Task<string> GetInventoryContents(BlobContainerClient inventoryBcc, string productId)
    {
        //var bcc = clientFactory.Invoke("inventories");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(productId);
        ArgumentNullException.ThrowIfNull(inventoryBcc);
        var bc = inventoryBcc.GetBlobClient(productId);
        var downloadInfo = await bc.DownloadContentAsync().ConfigAwait();
        return downloadInfo.Value.Content.ToString();
    }

    public async Task<string> GetStoreContents(BlobContainerClient storeBcc, string storeId)
    {
        //var bcc = clientFactory.Invoke("stores");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(storeId);
        ArgumentNullException.ThrowIfNull(storeBcc);
        var bc = storeBcc.GetBlobClient(storeId);
        var downloadInfo = await bc.DownloadContentAsync().ConfigAwait();
        return downloadInfo.Value.Content.ToString();
    }
}
