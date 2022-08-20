using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using Spurious2.Core;
using Spurious2.Core.LcboImporting.Adapters;
using Spurious2.Core.LcboImporting.Domain;

namespace Spurious2.AzureStorageAdapter;

public class StorageAdapter : IStorageAdapter
{
    private readonly Func<string, BlobContainerClient> clientFactory;
    private readonly ILogger<StorageAdapter> logger;

    public StorageAdapter(Func<string, BlobContainerClient> clientFactory, ILogger<StorageAdapter> logger)
    {
        this.logger = logger;
        this.clientFactory = clientFactory;
    }

    public async Task ClearStorage()
    {
        var productsClient = this.clientFactory.Invoke("products");
        var inventoriesClient = this.clientFactory.Invoke("inventories");
        var storesClient = this.clientFactory.Invoke("stores");
        var lastProductClient = this.clientFactory.Invoke("last-product");
        var lastInventoryClient = this.clientFactory.Invoke("last-inventory");
        this.logger.LogInformation("Deleting containers");
        Task.WaitAll(new Task[]
        {
            productsClient.DeleteIfExistsAsync(),
            inventoriesClient.DeleteIfExistsAsync(),
            storesClient.DeleteIfExistsAsync(),
            lastProductClient.DeleteIfExistsAsync(),
            lastInventoryClient.DeleteIfExistsAsync()
        });

        this.logger.LogInformation("Deleted containers");

        for (var i = 0; i < 3; i++)
        {
            this.logger.LogInformation("Creating containers attempt {attempt}", i + 1);

            var errors = false;
            try
            {
                await productsClient.CreateIfNotExistsAsync().ConfigAwait();
                this.logger.LogInformation("Created products");
            }
            catch (Exception ex)
            {
                this.logger.LogWarning(ex, "Couldn't create products");
                errors = true;
            }

            try
            {
                await inventoriesClient.CreateIfNotExistsAsync().ConfigAwait();
                this.logger.LogInformation("Created inventories");
            }
            catch (Exception ex)
            {
                this.logger.LogWarning(ex, "Couldn't create inventories");
                errors = true;
            }

            try
            {
                await storesClient.CreateIfNotExistsAsync().ConfigAwait();
                this.logger.LogInformation("Created stores");
            }
            catch (Exception ex)
            {
                this.logger.LogWarning(ex, "Couldn't create stores");
                errors = true;
            }

            try
            {
                await lastProductClient.CreateIfNotExistsAsync().ConfigAwait();
                this.logger.LogInformation("Created last-product");
            }
            catch (Exception ex)
            {
                this.logger.LogWarning(ex, "Couldn't create last-product");
                errors = true;
            }

            try
            {
                await lastInventoryClient.CreateIfNotExistsAsync().ConfigAwait();
                this.logger.LogInformation("Created last-inventory");
            }
            catch (Exception ex)
            {
                this.logger.LogWarning(ex, "Couldn't create last inventory  ");
                errors = true;
            }

            if (i == 2 && errors)
            {
                this.logger.LogError("Failed to create one of the containers after 3 tries");
                throw new InvalidOperationException();
            }

            if (errors)
            {
                await Task.Delay(30000).ConfigAwait();
            }
            else
            {
                break;
            }
        }
    }

    public async Task WriteProduct(string productId, string pageContent, ProductType productType)
    {
        var bcc = this.clientFactory.Invoke(productType.ToString().ToLowerInvariant());
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

    public async Task WriteProductId(string productId)
    {
        var bcc = this.clientFactory.Invoke("products");
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
        var bcc = this.clientFactory.Invoke("inventories");
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
        var bcc = this.clientFactory.Invoke("stores");
        var bc = bcc.GetBlobClient(storeId);
        return await bc.ExistsAsync().ConfigAwait();
    }

    public async Task WriteStore(string storeId, string pageContent)
    {
        var bcc = this.clientFactory.Invoke("stores");
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
        var bcc = this.clientFactory.Invoke("last-inventory");
        var bc = bcc.GetBlobClient(Guid.NewGuid().ToString());
        await bc.UploadTextAsync(input).ConfigAwait();
    }

    public async Task WriteLastProduct(string input)
    {
        var bcc = this.clientFactory.Invoke("last-product");
        var bc = bcc.GetBlobClient(Guid.NewGuid().ToString());
        await bc.UploadTextAsync(input).ConfigAwait();
    }
}
