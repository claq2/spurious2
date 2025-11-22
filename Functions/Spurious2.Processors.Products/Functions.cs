using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Spurious2.Core2.Lcbo;

namespace Spurious2.Processors.Products;

public class Functions(IImportingService importingService)
{
    public async Task ProcessQueueMessage([QueueTrigger("products", Connection = "queues")] string productId,
        [Blob("inventories", FileAccess.Write, Connection = "blobs")] BlobContainerClient inventoriesClient,
        [Queue("inventories", Connection = "queues")] QueueClient inventoriesQueue,
        ILogger logger)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(productId);
        ArgumentNullException.ThrowIfNull(inventoriesClient);
        ArgumentNullException.ThrowIfNull(importingService);
        await importingService.ProcessProductBlob(inventoriesClient, inventoriesQueue, productId).ConfigureAwait(false);
        logger.LogInformation(productId);
    }
}
