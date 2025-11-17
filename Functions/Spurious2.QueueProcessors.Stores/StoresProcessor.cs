using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Spurious2.Core2;
using Spurious2.Core2.Lcbo;

namespace Spurious2.QueueProcessors.Stores;

public class StoresProcessor(ILogger<StoresProcessor> logger, IImportingService importingService)
{
    [Function(nameof(StoresProcessor))]
    public async Task Run([QueueTrigger("stores", Connection = "queues")] string storeId)
    {
        logger.LogInformation("Store queue trigger called for {StoreId}", storeId);
        await importingService.ProcessStoreBlob(storeId).ConfigAwait();
        logger.LogInformation("C# queue trigger function processed store blob Name: {StoreId}", storeId);
    }
}
