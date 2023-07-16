using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace Spurious2.Function
{
    public interface IBlobImportFunctions
    {
        Task GetSpiritsPages(string name, ILogger logger);
        Task GetBeerPages(string name, ILogger logger);
        Task<string> RunOrchestrator(IDurableOrchestrationContext context, ILogger logger);
        Task<HttpResponseMessage> HttpStart(HttpRequestMessage req,
            IDurableOrchestrationClient starter, ILogger logger);
        Task GetWinePages([ActivityTrigger] string name, ILogger logger);
        Task Inventory(Stream myBlob, string name, ILogger logger);
        Task LastInventory(Stream myBlob, string name, ILogger logger);
        Task LastProduct(Stream myBlob, string name, ILogger logger);
    }
}
