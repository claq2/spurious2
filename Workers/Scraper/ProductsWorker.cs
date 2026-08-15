using System.Text.Json;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Spurious2.Core2.Lcbo;

namespace Scraper;

public class ProductsWorker(IServiceScopeFactory serviceScopeFactory,
    [FromKeyedServices("productsqueuesclient")] QueueClient productsQueueClient,
    [FromKeyedServices("inventoriesblobsclient")] BlobContainerClient inventoriesBlobContainerClient,
    [FromKeyedServices("inventoriesqueuesclient")] QueueClient inventoriesQueueClient,
    IConfiguration configuration,
    ILogger<ProductsWorker> logger) : BackgroundService
{
    private readonly string connectionString = configuration.GetConnectionString("queues")!;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            try
            {
                QueueMessage? message = await productsQueueClient.ReceiveMessageAsync(
                        visibilityTimeout: TimeSpan.FromSeconds(30),
                        cancellationToken: stoppingToken).ConfigureAwait(false);

                if (message?.Body != null)
                {
                    //var pid = message.Body.ToString();
                    try
                    {

                        //var productId = JsonSerializer.Deserialize<string>(message.Body.ToString());
                        var productId = message.Body.ToString();
                        if (productId != null)
                        {
                            using var scope = serviceScopeFactory.CreateScope();
                            var importingService = scope.ServiceProvider.GetRequiredService<IImportingService>();
                            await importingService.ProcessProductBlob(inventoriesBlobContainerClient, inventoriesQueueClient, productId).ConfigureAwait(false);
                        }
                        else
                        {
                            logger.LogWarning("Received message with null productId: {messageId}", message.MessageId);
                        }

                        await productsQueueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt, stoppingToken).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        // Do NOT delete the message - it will become visible again after the timeout
                        // and will be retried automatically
                        logger.LogError(ex, "Failed to process message {MessageId}, will retry",
                            message.MessageId);

                        // Check if the message has been dequeued too many times
                        if (message.DequeueCount > 5)
                        {
                            logger.LogError("Message {MessageId} exceeded retry limit, moving to poison queue",
                                message.MessageId);
                            await this.MoveToPoison(message, stoppingToken).ConfigureAwait(false);
                        }
                    }
                }
                else
                {
                    // No messages available - wait before polling again
                    // This prevents hammering the queue when it is empty
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException)
            {
                // Shutdown requested - exit gracefully
                break;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error receiving message from queue");
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken).ConfigureAwait(false);
            }
        }
    }

    private async Task MoveToPoison(QueueMessage message, CancellationToken ct)
    {
        // Create a poison queue for messages that repeatedly fail
        var poisonClient = new QueueClient(
            this.connectionString,
            "products-poison",
            new QueueClientOptions
            {
                MessageEncoding = QueueMessageEncoding.Base64
            });
        await poisonClient.CreateIfNotExistsAsync(cancellationToken: ct).ConfigureAwait(false);
        await poisonClient.SendMessageAsync(message.Body.ToString(), ct).ConfigureAwait(false);
        await productsQueueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt, ct).ConfigureAwait(false);
    }
}
