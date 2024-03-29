using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using Spurious2.Core2;
using Spurious2.Core2.Lcbo;

namespace Spurious2.Infrastructure.AzureStorage;

public class StorageAdapter(Func<string, BlobContainerClient> clientFactory, ILogger<StorageAdapter> logger) : IStorageAdapter
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>")]
    public async Task ClearStorage()
    {
        var productsClient = clientFactory.Invoke("products");
        var inventoriesClient = clientFactory.Invoke("inventories");
        var storesClient = clientFactory.Invoke("stores");
        var lastProductClient = clientFactory.Invoke("last-product");
        var lastInventoryClient = clientFactory.Invoke("last-inventory");
        logger.DeletingContainers();
        await Task.WhenAll(
        [
            productsClient.DeleteIfExistsAsync(),
            inventoriesClient.DeleteIfExistsAsync(),
            storesClient.DeleteIfExistsAsync(),
            lastProductClient.DeleteIfExistsAsync(),
            lastInventoryClient.DeleteIfExistsAsync()
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

            try
            {
                await lastProductClient.CreateIfNotExistsAsync().ConfigAwait();
                logger.CreatedLastProduct();
            }
            catch (Exception ex)
            {
                logger.CouldNotCreateLastProduct(ex);
                errors = true;
            }

            try
            {
                await lastInventoryClient.CreateIfNotExistsAsync().ConfigAwait();
                logger.CreatedLastInventory();
            }
            catch (Exception ex)
            {
                logger.CouldNotCreateLastInventory(ex);
                errors = true;
            }

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

    public async Task WriteProductId(string productId)
    {
        var bcc = clientFactory.Invoke("products");
        var bc = bcc.GetBlobClient(productId);
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

    public async Task WriteInventory(string productId, string pageContent)
    {
        var bcc = clientFactory.Invoke("inventories");
        var bc = bcc.GetBlobClient(productId);
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

    public async Task<bool> StoreExists(string storeId)
    {
        var bcc = clientFactory.Invoke("stores");
        var bc = bcc.GetBlobClient(storeId);
        return await bc.ExistsAsync().ConfigAwait();
    }

    public async Task WriteStore(string storeId, string pageContent)
    {
        var bcc = clientFactory.Invoke("stores");
        var bc = bcc.GetBlobClient(storeId);
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

    public async Task WriteLastInventory(string input)
    {
        var bcc = clientFactory.Invoke("last-inventory");
        var bc = bcc.GetBlobClient(Guid.NewGuid().ToString());
        await bc.UploadTextAsync(input).ConfigAwait();
    }

    public async Task WriteLastProduct(string input)
    {
        var bcc = clientFactory.Invoke("last-product");
        var bc = bcc.GetBlobClient(Guid.NewGuid().ToString());
        await bc.UploadTextAsync(input).ConfigAwait();
    }
}
