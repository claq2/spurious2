using Azure.Storage.Queues;
using Microsoft.Extensions.Logging;
using Spurious2.Core2;
using Spurious2.Core2.Lcbo;

namespace Spurious2.Infrastructure.AzureStorage;

public class QueueAdapter(
    //Func<string, QueueClient> clientFactory,
    ILogger<QueueAdapter> logger) : IQueueAdapter
{
    public async Task ClearQueues(QueueClient productsQueue,
        QueueClient inventoriesQueue,
        QueueClient storesQueue)
    {
        ArgumentNullException.ThrowIfNull(productsQueue);
        ArgumentNullException.ThrowIfNull(inventoriesQueue);
        ArgumentNullException.ThrowIfNull(storesQueue);
        //productsQueue ??= clientFactory.Invoke("products");
        //inventoriesQueue ??= clientFactory.Invoke("inventories");
        //storesQueue ??= clientFactory.Invoke("stores");
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

    public async Task WriteProductId(QueueClient bcc, string productId)
    {
        //var bcc = clientFactory.Invoke("products");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(productId);
        ArgumentNullException.ThrowIfNull(bcc);
        await bcc.SendMessageAsync(productId).ConfigAwait();
    }

    public async Task WriteInventoryId(QueueClient bcc, string productId)
    {
        //var bcc = clientFactory.Invoke("inventories");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(productId);
        ArgumentNullException.ThrowIfNull(bcc);
        await bcc.SendMessageAsync(productId).ConfigAwait();
    }

    public async Task WriteStoreId(QueueClient bcc, string storeId)
    {
        //var bcc = clientFactory.Invoke("stores");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(storeId);
        ArgumentNullException.ThrowIfNull(bcc);
        await bcc.SendMessageAsync(storeId).ConfigAwait();
    }
}
