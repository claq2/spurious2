using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Spurious2.Processors.Products;

public static class Functions
{
    public static void ProcessQueueMessage([QueueTrigger("products", Connection = "queues")] string message, ILogger logger)
    {
        logger.LogInformation(message);
    }
}
