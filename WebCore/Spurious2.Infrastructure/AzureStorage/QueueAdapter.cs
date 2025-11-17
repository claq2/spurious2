using Azure.Storage.Queues;
using Microsoft.Extensions.Logging;
using Spurious2.Core2;
using Spurious2.Core2.Lcbo;

namespace Spurious2.Infrastructure.AzureStorage;

public class QueueAdapter(Func<string, QueueClient> clientFactory, ILogger<QueueAdapter> logger) : IQueueAdapter
{
    public async Task ClearQueues()
    {
        var productsQueue = clientFactory.Invoke("products");
        var inventoriesQueue = clientFactory.Invoke("inventories");
        var storesQueue = clientFactory.Invoke("stores");
        logger.LogInformation("Creating queues");
        await Task.WhenAll(
        [
            productsQueue.CreateIfNotExistsAsync(),
            inventoriesQueue.CreateIfNotExistsAsync(),
            storesQueue.CreateIfNotExistsAsync(),
        ]).ConfigAwait();
        logger.LogInformation("Created queues");
        logger.LogInformation("Clearing queues");
        await Task.WhenAll(
        [
            productsQueue.ClearMessagesAsync(),
            inventoriesQueue.ClearMessagesAsync(),
            storesQueue.ClearMessagesAsync(),
        ]).ConfigAwait();

        logger.LogInformation("Queues cleared");
    }

    public async Task WriteProductId(string productId)
    {
        var bcc = clientFactory.Invoke("products");
        await bcc.SendMessageAsync(productId).ConfigAwait();
    }
}
