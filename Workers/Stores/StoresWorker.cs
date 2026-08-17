using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Spurious2.Core2.Lcbo;

namespace Stores;

public class StoresWorker(IServiceScopeFactory serviceScopeFactory,
    [FromKeyedServices("storesqueuesclient")] QueueClient storesQueueClient,
    [FromKeyedServices("storesblobsclient")] BlobContainerClient storesBlobContainerClient,
    IConfiguration configuration,
    ILogger<StoresWorker> logger) : BackgroundService
{
    private readonly string connectionString = configuration.GetConnectionString("queues")!;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {Time}", DateTimeOffset.Now);
            }

            try
            {
                QueueMessage? message = await storesQueueClient.ReceiveMessageAsync(
                        visibilityTimeout: TimeSpan.FromSeconds(30),
                        cancellationToken: stoppingToken).ConfigureAwait(false);

                if (message?.Body != null)
                {
                    try
                    {
                        var storeId = message.Body.ToString();
                        if (storeId != null)
                        {
                            using var scope = serviceScopeFactory.CreateScope();
                            var importingService = scope.ServiceProvider.GetRequiredService<IImportingService>();
                            await importingService.ProcessStoreBlob(storesBlobContainerClient, storeId, stoppingToken).ConfigureAwait(false);
                        }
                        else
                        {
                            logger.LogWarning("Received message with null productId: {MessageId}", message.MessageId);
                        }

                        await storesQueueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt, stoppingToken).ConfigureAwait(false);
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
            "stores-poison",
            new QueueClientOptions
            {
                MessageEncoding = QueueMessageEncoding.Base64
            });
        await poisonClient.CreateIfNotExistsAsync(cancellationToken: ct).ConfigureAwait(false);
        await poisonClient.SendMessageAsync(message.Body.ToString(), ct).ConfigureAwait(false);
        await storesQueueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt, ct).ConfigureAwait(false);
    }
}
