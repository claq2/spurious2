using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Spurious2.QueueProcessors.Inventories;

public class InventoriesProcessor
{
    private readonly ILogger<InventoriesProcessor> _logger;

    public InventoriesProcessor(ILogger<InventoriesProcessor> logger)
    {
        _logger = logger;
    }

    [Function(nameof(InventoriesProcessor))]
    public void Run([QueueTrigger("inventories", Connection = "queues")] QueueMessage message)
    {
        _logger.LogInformation("C# Queue trigger function processed: {messageText}", message.MessageText);
    }
}
