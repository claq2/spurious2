using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
//using Microsoft.Azure.WebJobs;
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
        catch (Exception ex)
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

    [Function("Function_HttpStart")]
    public async Task<HttpResponseData> HttpStart2(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
      [DurableClient] DurableTaskClient client,
      FunctionContext executionContext)
    {
        //var logger = loggerFactory.CreateLogger<Function2>();
        ILogger<BlobImportFunctions> logger = executionContext.GetLogger<BlobImportFunctions>();

        // Function input comes from the request content.
        var instanceId = await client.ScheduleNewOrchestrationInstanceAsync(
            "Function2");

        logger.LogInformation("Started orchestration with ID = '{instanceId}'.", instanceId);

        // Returns an HTTP 202 response with an instance management payload.
        // See https://learn.microsoft.com/azure/azure-functions/durable/durable-functions-http-api#start-orchestration
        return await client.CreateCheckStatusResponseAsync(req, instanceId);
    }

    [Function("Function2")]
    public async Task<string> RunOrchestrator2(
        [OrchestrationTrigger] TaskOrchestrationContext context)
    {
        ILogger logger = context.CreateReplaySafeLogger<BlobImportFunctions>();
        logger.LogInformation("Saying hello.");
        var result = "";
        result += await context.CallActivityAsync<string>(nameof(SayHello), "Tokyo") + " ";
        result += await context.CallActivityAsync<string>(nameof(SayHello), "London") + " ";
        result += await context.CallActivityAsync<string>(nameof(SayHello), "Seattle");
        return result;
    }

    [Function(nameof(SayHello))]
    public string SayHello([ActivityTrigger] string name, FunctionContext executionContext)
    {
        ILogger<BlobImportFunctions> logger = executionContext.GetLogger<BlobImportFunctions>();
        logger.LogInformation("Saying hello to {name}.", name);
        return $"Hello {name}!";
    }

    [Function(nameof(StartImporting))]
    public async Task StartImporting([ActivityTrigger] string name, FunctionContext executionContext)
    {
        ILogger<BlobImportFunctions> logger = executionContext.GetLogger<BlobImportFunctions>();
        await importingService.StartImporting().ConfigAwait();
        logger.LogInformation("Finished StartImporting.");
        //return "StartImporting";
    }

    [Function(nameof(GetWinePages))]
    public async Task GetWinePages([ActivityTrigger] string name, ILogger logger)
    {
        await importingService.GetProductPages(ProductType.Wine).ConfigAwait();
        logger.LogInformation($"Finished GetWinePages.");
    }

    [Function(nameof(GetBeerPages))]
    public async Task GetBeerPages([ActivityTrigger] string name, ILogger logger)
    {
        await importingService.GetProductPages(ProductType.Beer).ConfigAwait();
        logger.LogInformation($"Finished GetBeerPages.");
    }

    [Function(nameof(GetSpiritsPages))]
    public async Task GetSpiritsPages([ActivityTrigger] string name, ILogger logger)
    {
        await importingService.GetProductPages(ProductType.Spirits).ConfigAwait();
        logger.LogInformation($"Finished GetSpiritsPages.");
    }

    [Function(nameof(Product))]
    public async Task Product([BlobTrigger("products/{productId}", Connection = "AzureWebJobsStorage")] string myBlob,
        string productId)
    {
        await importingService.ProcessProductBlob(productId).ConfigAwait();
        this.logger.LogInformation($"C# Blob trigger function processed product blob\n Name:{productId} \n Size: {myBlob.Length} Bytes");
    }


    [Function(nameof(SignalLastProductDone))]
    public async Task SignalLastProductDone([ActivityTrigger] string name, FunctionContext executionContext)
    {
        ILogger<BlobImportFunctions> logger = executionContext.GetLogger<BlobImportFunctions>();
        await importingService.SignalLastProductDone().ConfigAwait();
        logger.LogInformation($"Finished SignalLastProductDone.");
    }

    [Function(nameof(Inventory))]
    public async Task Inventory([BlobTrigger("inventories/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob,
        string name)
    {
        this.logger.LogInformation("Inventory blob trigger called for {name}", name);
        await importingService.ProcessInventoryBlob(name, myBlob).ConfigAwait();
        this.logger.LogInformation($"C# Blob trigger function processed inventory blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
    }

    [Function(nameof(Store))]
    public async Task Store([BlobTrigger("stores/{storeId}", Connection = "AzureWebJobsStorage")] Stream myBlob,
        string storeId)
    {
        await importingService.ProcessStoreBlob(storeId, myBlob).ConfigAwait();
        this.logger.LogInformation($"C# Blob trigger function processed store blob\n Name:{storeId} \n Size: {myBlob.Length} Bytes");
    }

    [Function(nameof(LastProduct))]
    public async Task LastProduct([BlobTrigger("last-product/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob,
        string name)
    {
        await importingService.ProcessLastProductBlob(name).ConfigAwait();
        this.logger.LogInformation($"C# Blob trigger function Processed last product blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
    }

    [Function(nameof(LastInventory))]
    public async Task LastInventory([BlobTrigger("last-inventory/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob,
        string name)
    {
        await importingService.ProcessLastInventoryBlob(name).ConfigAwait();
        this.logger.LogInformation($"C# Blob trigger function Processed last inventory blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
    }

    [Function(nameof(UpdateHttpStart))]
    public async Task<HttpResponseData> UpdateHttpStart(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        [DurableClient] DurableTaskClient client, FunctionContext executionContext)
    {
        ILogger<BlobImportFunctions> logger = executionContext.GetLogger<BlobImportFunctions>();
        // Function input comes from the request content.
        var instanceId = await client.ScheduleNewOrchestrationInstanceAsync(nameof(UpdateOrch));

        logger.LogInformation($"Started update orchestration with ID = '{instanceId}'.");

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
