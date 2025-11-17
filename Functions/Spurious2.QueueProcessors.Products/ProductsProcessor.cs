using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Spurious2.QueueProcessors.Products;

public class ProductsProcessor
{
    private readonly ILogger<ProductsProcessor> _logger;

    public ProductsProcessor(ILogger<ProductsProcessor> logger)
    {
        _logger = logger;
    }

    [Function(nameof(ProductsProcessor))]
    public void Run([QueueTrigger("products", Connection = "queues")] QueueMessage message)
    {
        _logger.LogInformation("C# Queue trigger function processed: {messageText}", message.MessageText);
    }
}
