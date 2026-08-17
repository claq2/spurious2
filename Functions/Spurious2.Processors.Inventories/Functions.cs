using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Spurious2.Core2;
using Spurious2.Core2.Lcbo;

namespace Spurious2.Processors.Inventories;

public class Functions(IImportingService importingService)
{
    public async Task ProcessQueueMessage([QueueTrigger("inventories", Connection = "queues")] string productId,
        [Blob("inventories", FileAccess.Write, Connection = "blobs")] BlobContainerClient inventoriesClient,
        [Blob("stores", FileAccess.Write, Connection = "blobs")] BlobContainerClient storesClient,
        [Queue("stores", Connection = "queues")] QueueClient storesQueue,
        ILogger logger)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(productId);
        ArgumentNullException.ThrowIfNull(inventoriesClient);
        ArgumentNullException.ThrowIfNull(importingService);
        await importingService.ProcessInventoryBlob(inventoriesClient, storesClient, storesQueue, productId, CancellationToken.None).ConfigAwait();
        logger.LogInformation(productId);
    }
}
