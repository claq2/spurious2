using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Spurious2.QueueProcessors.Stores;

public class StoresProcessor
{
    private readonly ILogger<StoresProcessor> _logger;

    public StoresProcessor(ILogger<StoresProcessor> logger)
    {
        _logger = logger;
    }

    [Function(nameof(StoresProcessor))]
    public void Run([QueueTrigger("stores", Connection = "queues")] QueueMessage message)
    {
        _logger.LogInformation("C# Queue trigger function processed: {messageText}", message.MessageText);
    }
}
