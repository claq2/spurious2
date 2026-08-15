using Azure.Storage.Blobs;
using Azure.Storage.Queues;

namespace Scraper;

public class ProductsWorker([FromKeyedServices("productsblobsclient")] BlobContainerClient productsBlobContainerClient,
    [FromKeyedServices("productsqueuesclient")] QueueClient productsQueueClient,
    ILogger<ProductsWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
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
