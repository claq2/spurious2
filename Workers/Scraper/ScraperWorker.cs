using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Spurious2.Core2.Lcbo;

namespace Scraper;

public class ScraperWorker(IImportingService importingService,
    [FromKeyedServices("productblobsclient")] BlobContainerClient productsBlobContainerClient,
    [FromKeyedServices("inventoryblobsclient")] BlobContainerClient inventoryBlobContainerClient,
    [FromKeyedServices("storesblobsclient")] BlobContainerClient storesBlobContainerClient,
    [FromKeyedServices("productqueuesclient")] QueueClient productsQueueClient,
    [FromKeyedServices("inventoryqueuesclient")] QueueClient inventoryQueueClient,
    [FromKeyedServices("storesqueuesclient")] QueueClient storesQueueClient,
    ILogger<ScraperWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await importingService.StartImporting(productsBlobContainerClient,
            inventoryBlobContainerClient,
            storesBlobContainerClient,
            productsQueueClient,
            inventoryQueueClient,
            storesQueueClient).ConfigureAwait(false);
        logger.LogInformation("Starting");
        await foreach (var productId in importingService.GetProductPagesAndReturnIds(ProductType.Beer).ConfigureAwait(false))
        {
            await productsQueueClient.SendMessageAsync(productId, stoppingToken).ConfigureAwait(false);
        }

        await foreach (var productId in importingService.GetProductPagesAndReturnIds(ProductType.Wine).ConfigureAwait(false))
        {
            await productsQueueClient.SendMessageAsync(productId, stoppingToken).ConfigureAwait(false);
        }

        await foreach (var productId in importingService.GetProductPagesAndReturnIds(ProductType.Spirits).ConfigureAwait(false))
        {
            await productsQueueClient.SendMessageAsync(productId, stoppingToken).ConfigureAwait(false);
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await Task.Delay(1000, stoppingToken).ConfigureAwait(false);
        }
    }
}
