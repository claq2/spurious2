using Azure.Storage.Blobs;
using Azure.Storage.Queues;

namespace Stores;

public class StoresWorker(IServiceScopeFactory serviceScopeFactory,
    [FromKeyedServices("productsqueuesclient")] QueueClient productsQueueClient,
    [FromKeyedServices("inventoriesblobsclient")] BlobContainerClient inventoriesBlobContainerClient,
    [FromKeyedServices("inventoriesqueuesclient")] QueueClient inventoriesQueueClient,
    IConfiguration configuration,
    ILogger<StoresWorker> logger) : BackgroundService
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
