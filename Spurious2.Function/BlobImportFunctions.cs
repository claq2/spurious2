using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Spurious2.Core;
using Spurious2.Core.LcboImporting.Domain;
using Spurious2.Core.LcboImporting.Services;

namespace Spurious2.Function;

public class BlobImportFunctions : IBlobImportFunctions
{
    private readonly IImportingService importingService;

    public BlobImportFunctions(IImportingService importingService)
    {
        this.importingService = importingService;
    }

    //[FunctionName("TimerFunction1")]
    //public async Task Run([TimerTrigger("0 */2 * * * *")] TimerInfo myTimer,
    //    [DurableClient] IDurableOrchestrationClient starter)
    //{
    //    var instanceId = await starter.StartNewAsync("OrchFunction1", null);
    //    this.logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now} ID {instanceId}");
    //}

    [FunctionName("BlobImportFunctionsHttpStart")]
    public async Task<HttpResponseMessage> HttpStart(
       [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
       [DurableClient] IDurableOrchestrationClient starter, ILogger logger)
    {
        // Function input comes from the request content.
        var instanceId = await starter.StartNewAsync(nameof(BlobImportFunctions), null);

        logger.LogInformation($"Started orchestration with ID = '{instanceId}'.");

        return starter.CreateCheckStatusResponse(req, instanceId);
    }

    [FunctionName(nameof(BlobImportFunctions))]
    [Singleton]
    public async Task<string> RunOrchestrator(
       [OrchestrationTrigger] IDurableOrchestrationContext context, ILogger logger)
    {
        await context.CallActivityAsync(nameof(StartImporting), "Blah");

        await Task.WhenAll(new List<Task>
        {
            context.CallActivityAsync(nameof(GetWinePages), "Blah"),
            context.CallActivityAsync(nameof(GetBeerPages), "Blah"),
            context.CallActivityAsync(nameof(GetSpiritsPages), "Blah"),
        });

        await context.CallActivityAsync(nameof(SignalLastProductDone), "Blah");
        logger.LogInformation($"Finished BlobImportFunctions.");
        return "Done BlobImportFunctions";
    }

    [FunctionName(nameof(StartImporting))]
    public async Task StartImporting([ActivityTrigger] string name, ILogger logger)
    {
        await this.importingService.StartImporting().ConfigAwait();
        logger.LogInformation($"Finished StartImporting.");
    }

    [FunctionName(nameof(GetWinePages))]
    public async Task GetWinePages([ActivityTrigger] string name, ILogger logger)
    {
        await this.importingService.GetProductPages(ProductType.Wine).ConfigAwait();
        logger.LogInformation($"Finished GetWinePages.");
    }

    [FunctionName(nameof(GetBeerPages))]
    public async Task GetBeerPages([ActivityTrigger] string name, ILogger logger)
    {
        await this.importingService.GetProductPages(ProductType.Beer).ConfigAwait();
        logger.LogInformation($"Finished GetBeerPages.");
    }

    [FunctionName(nameof(GetSpiritsPages))]
    public async Task GetSpiritsPages([ActivityTrigger] string name, ILogger logger)
    {
        await this.importingService.GetProductPages(ProductType.Spirits).ConfigAwait();
        logger.LogInformation($"Finished GetSpiritsPages.");
    }

    [FunctionName(nameof(Product))]
    public async Task Product([BlobTrigger("products/{productId}", Connection = "AzureWebJobsStorage")] string myBlob,
        string productId, ILogger logger)
    {
        await this.importingService.ProcessProductBlob(productId).ConfigAwait();
        logger.LogInformation($"C# Blob trigger function processed product blob\n Name:{productId} \n Size: {myBlob.Length} Bytes");
    }


    [FunctionName(nameof(SignalLastProductDone))]
    public async Task SignalLastProductDone([ActivityTrigger] string name, ILogger logger)
    {
        await this.importingService.SignalLastProductDone().ConfigAwait();
        logger.LogInformation($"Finished SignalLastProductDone.");
    }

    [FunctionName(nameof(Inventory))]
    public async Task Inventory([BlobTrigger("inventories/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob,
        string name, ILogger logger)
    {
        logger.LogInformation("Inventory blob trigger called for {name}", name);
        await this.importingService.ProcessInventoryBlob(name, myBlob).ConfigAwait();
        logger.LogInformation($"C# Blob trigger function processed inventory blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
    }

    [FunctionName(nameof(Store))]
    public async Task Store([BlobTrigger("stores/{storeId}", Connection = "AzureWebJobsStorage")] Stream myBlob,
        string storeId, ILogger logger)
    {
        await this.importingService.ProcessStoreBlob(storeId, myBlob).ConfigAwait();
        logger.LogInformation($"C# Blob trigger function processed store blob\n Name:{storeId} \n Size: {myBlob.Length} Bytes");
    }

    [FunctionName(nameof(LastProduct))]
    public async Task LastProduct([BlobTrigger("last-product/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob,
        string name, ILogger logger)
    {
        await this.importingService.ProcessLastProductBlob(name).ConfigAwait();
        logger.LogInformation($"C# Blob trigger function Processed last product blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
    }

    [FunctionName(nameof(LastInventory))]
    public async Task LastInventory([BlobTrigger("last-inventory/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob,
        string name, ILogger logger)
    {
        await this.importingService.ProcessLastInventoryBlob(name).ConfigAwait();
        logger.LogInformation($"C# Blob trigger function Processed last inventory blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
    }

    [FunctionName(nameof(UpdateHttpStart))]
    public async Task<HttpResponseMessage> UpdateHttpStart(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
    [DurableClient] IDurableOrchestrationClient starter, ILogger logger)
    {
        // Function input comes from the request content.
        var instanceId = await starter.StartNewAsync(nameof(UpdateOrch), null);

        logger.LogInformation($"Started update orchestration with ID = '{instanceId}'.");

        return starter.CreateCheckStatusResponse(req, instanceId);
    }

    [FunctionName(nameof(UpdateOrch))]
    [Singleton]
    public async Task<string> UpdateOrch(
       [OrchestrationTrigger] IDurableOrchestrationContext context, ILogger logger)
    {
        await context.CallActivityAsync(nameof(Update), "Blah");
        logger.LogInformation($"Finished UpdateOrch.");
        return "Done UpdateOrch";
    }

    [FunctionName(nameof(Update))]
    public async Task Update([ActivityTrigger] string name, ILogger logger)
    {
        await this.importingService.UpdateAll().ConfigAwait();
        logger.LogInformation($"Finished Update.");
    }
}
