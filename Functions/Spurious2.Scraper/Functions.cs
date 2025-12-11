using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Spurious2.Core2.Lcbo;

namespace Spurious2.Scraper;

public class Functions(IImportingService importingService)
{
    [NoAutomaticTrigger]
    public static void Start(ILogger logger)
    {
        logger.LogInformation("Starting");
    }

    public async Task StartByQueue([QueueTrigger("start", Connection = "queues")] string message,
        [Blob("products", FileAccess.Write, Connection = "blobs")] BlobContainerClient productsClient,
        [Blob("inventories", FileAccess.Write, Connection = "blobs")] BlobContainerClient inventoriesClient,
        [Blob("stores", FileAccess.Write, Connection = "blobs")] BlobContainerClient storesClient,
        [Queue("products", Connection = "queues")] QueueClient productsQueue,
        [Queue("inventories", Connection = "queues")] QueueClient inventoriesQueue,
        [Queue("stores", Connection = "queues")] QueueClient storesQueue
        ,
        ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(productsClient);
        ArgumentNullException.ThrowIfNull(storesClient);
        ArgumentNullException.ThrowIfNull(inventoriesClient);
        ArgumentNullException.ThrowIfNull(productsQueue);
        ArgumentNullException.ThrowIfNull(importingService);
        await importingService.StartImporting(productsClient,
            inventoriesClient,
            storesClient,
            productsQueue,
            inventoriesQueue,
            storesQueue).ConfigureAwait(false);
        //var client = bc.GetBlobClient("start-message");
        //await client.UploadAsync(BinaryData.FromString(message), overwrite: true).ConfigureAwait(false);
        logger.LogInformation("Starting because of {Message}", message);
        await foreach (var productId in importingService.GetProductPagesAndReturnIds(ProductType.Beer).ConfigureAwait(false))
        {
            await productsQueue.SendMessageAsync(productId).ConfigureAwait(false);
        }
    }

    [NoAutomaticTrigger]
    public async Task UpdateAll(ILogger logger)
    {
        logger.LogInformation("Updating all");
        await importingService.UpdateAll().ConfigureAwait(false);
        logger.LogInformation("Updated all");
    }
}
