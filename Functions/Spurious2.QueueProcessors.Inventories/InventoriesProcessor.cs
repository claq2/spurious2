using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Spurious2.Core2.Lcbo;

namespace Spurious2.QueueProcessors.Inventories;

public class InventoriesProcessor(ILogger<InventoriesProcessor> logger, IImportingService importingService)
{
    [Function(nameof(InventoriesProcessor))]
    public async Task Run([QueueTrigger("inventories", Connection = "queues")] string productId)
    {
        logger.LogInformation("Inventory queue trigger called for {ProductId}", productId);
        //await importingService.ProcessInventoryBlob(productId).ConfigAwait();
        logger.LogInformation("C# queue trigger function processed inventory blob ProductId: {ProductId}", productId);
    }
}
