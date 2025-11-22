using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Spurious2.Core2.Lcbo;

namespace Spurious2.Scraper;

public static class Functions
{
    [NoAutomaticTrigger]
    public static void Start(ILogger logger)
    {
        logger.LogInformation("Starting");
    }

    public static async Task StartByQueue([QueueTrigger("start", Connection = "queues")] string message,
        [Blob("products", FileAccess.Write, Connection = "blobs")] BlobContainerClient bc,
        [Queue("products", Connection = "queues")] QueueClient productsQueue,
        IImportingService importingService,
        ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(bc);
        ArgumentNullException.ThrowIfNull(productsQueue);
        ArgumentNullException.ThrowIfNull(importingService);
        var client = bc.GetBlobClient("start-message");
        await client.UploadAsync(BinaryData.FromString(message), overwrite: true).ConfigureAwait(false);
        logger.LogInformation("Starting because of {Message}", message);
        await foreach (var productId in importingService.GetProductPagesAndReturnIds(ProductType.Beer).ConfigureAwait(false))
        {
            await productsQueue.SendMessageAsync(productId).ConfigureAwait(false);
        }
    }
}
