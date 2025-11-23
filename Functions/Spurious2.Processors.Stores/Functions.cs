using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Spurious2.Core2;
using Spurious2.Core2.Lcbo;

namespace Spurious2.Processors.Stores;

public class Functions(IImportingService importingService)
{
    public async Task ProcessQueueMessage([QueueTrigger("stores", Connection = "queues")] string storeId,
        [Blob("stores", FileAccess.Write, Connection = "blobs")] BlobContainerClient storesClient,
        ILogger logger)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(storeId);
        ArgumentNullException.ThrowIfNull(storesClient);
        ArgumentNullException.ThrowIfNull(importingService);
        await importingService.ProcessStoreBlob(storesClient, storeId).ConfigAwait();
        logger.LogInformation(storeId);
    }
}
