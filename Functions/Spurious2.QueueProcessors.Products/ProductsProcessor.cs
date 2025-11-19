using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Spurious2.Core2.Lcbo;

namespace Spurious2.QueueProcessors.Products;

public class ProductsProcessor
{
    private readonly ILogger<ProductsProcessor> _logger;
    private readonly IImportingService _importingService;

    public ProductsProcessor(ILogger<ProductsProcessor> logger, IImportingService importingService)
    {
        _logger = logger;
        _importingService = importingService;
    }

    [Function(nameof(ProductsProcessor))]
    public async Task Run([QueueTrigger("products", Connection = "queues")] string productId)
    {
        _logger.LogInformation("Product queue trigger called for {ProductId}", productId);
        await _importingService.ProcessProductBlob(productId).ConfigureAwait(false);
        _logger.LogInformation("C# queue trigger function processed product blob ProductId: {ProductId}", productId);
    }
}
