using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Spurious2.Core2;
using Spurious2.Core2.Lcbo;

namespace Scraper;

public class ScraperWorker(IServiceScopeFactory serviceScopeFactory,
    [FromKeyedServices("productsblobsclient")] BlobContainerClient productsBlobContainerClient,
    [FromKeyedServices("inventoriesblobsclient")] BlobContainerClient inventoryBlobContainerClient,
    [FromKeyedServices("storesblobsclient")] BlobContainerClient storesBlobContainerClient,
    [FromKeyedServices("productsqueuesclient")] QueueClient productsQueueClient,
    [FromKeyedServices("inventoriesqueuesclient")] QueueClient inventoryQueueClient,
    [FromKeyedServices("storesqueuesclient")] QueueClient storesQueueClient,
    ILogger<ScraperWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var importingService = scope.ServiceProvider.GetRequiredService<IImportingService>();

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

        //await foreach (var productId in importingService.GetProductPagesAndReturnIds(ProductType.Wine).ConfigureAwait(false))
        //{
        //    await productsQueueClient.SendMessageAsync(productId, stoppingToken).ConfigureAwait(false);
        //}

        //await foreach (var productId in importingService.GetProductPagesAndReturnIds(ProductType.Spirits).ConfigureAwait(false))
        //{
        //    await productsQueueClient.SendMessageAsync(productId, stoppingToken).ConfigureAwait(false);
        //}
    }
}

public class UpdateWorker(IServiceScopeFactory serviceScopeFactory,
    ILogger<UpdateWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Wait 60 seconds to let scraper get started
        await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken).ConfigureAwait(false);

        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var importingService = scope.ServiceProvider.GetRequiredService<IImportingService>();
            if (await importingService.AreAnyIncomingRecordsNotDone().ConfigAwait())
            {
                logger.LogInformation("Updating all");
                await importingService.UpdateAll().ConfigureAwait(false);
            }
            else
            {
                // Wait 30 seconds and check again
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken).ConfigureAwait(false);
            }
        }
    }
}
