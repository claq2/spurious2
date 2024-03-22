using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using Spurious2.Core2;
using Spurious2.Core2.Lcbo;

namespace Spurious2.Function2;

public class BlobImportFunctions(ILoggerFactory loggerFactory, IImportingService importingService)
{
    private readonly ILogger<BlobImportFunctions> logger = loggerFactory.CreateLogger<BlobImportFunctions>();

    [Function("BlobImportFunctionsHttpStart")]
    public async Task<HttpResponseData> HttpStart(
       [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
       [DurableClient] DurableTaskClient client,
       FunctionContext executionContext)
    {
        //var logger = loggerFactory.CreateLogger<Function2>();
        ILogger<BlobImportFunctions> logger = executionContext.GetLogger<BlobImportFunctions>();

        // Function input comes from the request content.
        var instanceId = await client.ScheduleNewOrchestrationInstanceAsync(
            "Lcbo");

        logger.LogInformation("Started orchestration with ID = '{instanceId}'.", instanceId);

        // Returns an HTTP 202 response with an instance management payload.
        // See https://learn.microsoft.com/azure/azure-functions/durable/durable-functions-http-api#start-orchestration
        return await client.CreateCheckStatusResponseAsync(req, instanceId);
    }

    [Function("Lcbo")]
    public async Task<string> RunOrchestrator(
        [OrchestrationTrigger] TaskOrchestrationContext context)
    {
        ILogger logger = context.CreateReplaySafeLogger<BlobImportFunctions>();
        try
        {
            await context.CallActivityAsync(nameof(StartImporting));
        }
        catch (Exception)
        {
            throw;
        }

        await Task.WhenAll(new List<Task>
        {
            context.CallActivityAsync(nameof(GetWinePages)),
            context.CallActivityAsync(nameof(GetBeerPages)),
            context.CallActivityAsync(nameof(GetSpiritsPages)),
        });

        await context.CallActivityAsync(nameof(SignalLastProductDone));
        logger.LogInformation($"Finished BlobImportFunctions.");
        return "Done BlobImportFunctions";
    }

    [Function(nameof(StartImporting))]
    public async Task StartImporting([ActivityTrigger] string name, FunctionContext executionContext)
    {
        ILogger<BlobImportFunctions> logger = executionContext.GetLogger<BlobImportFunctions>();
        await importingService.StartImporting().ConfigAwait();
        logger.LogInformation("Finished StartImporting.");
    }

    [Function(nameof(GetWinePages))]
    public async Task GetWinePages([ActivityTrigger] string name)
    {
        await importingService.GetProductPages(ProductType.Wine).ConfigAwait();
        this.logger.LogInformation("Finished GetWinePages.");
    }

    [Function(nameof(GetBeerPages))]
    public async Task GetBeerPages([ActivityTrigger] string name)
    {
        await importingService.GetProductPages(ProductType.Beer).ConfigAwait();
        this.logger.LogInformation("Finished GetBeerPages.");
    }

    [Function(nameof(GetSpiritsPages))]
    public async Task GetSpiritsPages([ActivityTrigger] string name)
    {
        await importingService.GetProductPages(ProductType.Spirits).ConfigAwait();
        this.logger.LogInformation("Finished GetSpiritsPages.");
    }

    [Function(nameof(Product))]
    public async Task Product([BlobTrigger("products/{productId}", Connection = "AzureWebJobsStorage")] string myBlob,
        string productId)
    {
        await importingService.ProcessProductBlob(productId).ConfigAwait();
        this.logger.LogInformation("C# Blob trigger function processed product blob\n Name:{ProductId} \n Size: {Length} Bytes", productId, myBlob.Length);
    }


    [Function(nameof(SignalLastProductDone))]
    public async Task SignalLastProductDone([ActivityTrigger] string name, FunctionContext executionContext)
    {
        ILogger<BlobImportFunctions> logger = executionContext.GetLogger<BlobImportFunctions>();
        await importingService.SignalLastProductDone().ConfigAwait();
        logger.LogInformation("Finished SignalLastProductDone.");
    }

    [Function(nameof(Inventory))]
    public async Task Inventory([BlobTrigger("inventories/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob,
        string name)
    {
        this.logger.LogInformation("Inventory blob trigger called for {name}", name);
        await importingService.ProcessInventoryBlob(name, myBlob).ConfigAwait();
        this.logger.LogInformation("C# Blob trigger function processed inventory blob\n Name: {Name} \n Size: {Length} Bytes", name, myBlob.Length);
    }

    [Function(nameof(Store))]
    public async Task Store([BlobTrigger("stores/{storeId}", Connection = "AzureWebJobsStorage")] Stream myBlob,
        string storeId)
    {
        await importingService.ProcessStoreBlob(storeId, myBlob).ConfigAwait();
        this.logger.LogInformation("C# Blob trigger function processed store blob\n Name: {StoreId} \n Size: {Length} Bytes", storeId, myBlob.Length);
    }

    [Function(nameof(LastProduct))]
    public async Task LastProduct([BlobTrigger("last-product/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob,
        string name)
    {
        await importingService.ProcessLastProductBlob(name).ConfigAwait();
        this.logger.LogInformation("C# Blob trigger function Processed last product blob\n Name: {Name} \n Size: {Length} Bytes", name, myBlob.Length);
    }

    [Function(nameof(LastInventory))]
    public async Task LastInventory([BlobTrigger("last-inventory/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob,
        string name)
    {
        await importingService.ProcessLastInventoryBlob(name).ConfigAwait();
        this.logger.LogInformation("C# Blob trigger function Processed last inventory blob\n Name: {Name} \n Size: {Length} Bytes", name, myBlob.Length);
    }

    [Function(nameof(UpdateHttpStart))]
    public async Task<HttpResponseData> UpdateHttpStart(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        [DurableClient] DurableTaskClient client, FunctionContext executionContext)
    {
        ILogger<BlobImportFunctions> logger = executionContext.GetLogger<BlobImportFunctions>();
        // Function input comes from the request content.
        var instanceId = await client.ScheduleNewOrchestrationInstanceAsync(nameof(UpdateOrch));

        logger.LogInformation("Started update orchestration with ID = '{InstanceId}'.", instanceId);

        return await client.CreateCheckStatusResponseAsync(req, instanceId);
    }

    [Function(nameof(UpdateOrch))]
    // [Singleton]
    public async Task<string> UpdateOrch(
       [OrchestrationTrigger] TaskOrchestrationContext context)
    {
        ILogger logger = context.CreateReplaySafeLogger<BlobImportFunctions>();
        await context.CallActivityAsync(nameof(Update));
        logger.LogInformation($"Finished UpdateOrch.");
        return "Done UpdateOrch";
    }

    [Function(nameof(Update))]
    public async Task Update([ActivityTrigger] string name)
    {
        await importingService.UpdateAll().ConfigAwait();
        this.logger.LogInformation($"Finished Update.");
    }
}
