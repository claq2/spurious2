using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Spurious2.Core2;
using Spurious2.Core2.Lcbo;

namespace Spurious2.QueueProcessors.Products;

public class ProductsProcessor(ILogger<ProductsProcessor> logger, IImportingService importingService)
{
    [Function(nameof(ProductsProcessor))]
    public async Task Run([QueueTrigger("products", Connection = "queues")] string productId)
    {
        logger.LogInformation("Product queue trigger called for {ProductId}", productId);
        await importingService.ProcessProductBlob(productId).ConfigAwait();
        logger.LogInformation("C# queue trigger function processed product blob ProductId: {ProductId}", productId);
    }
}
