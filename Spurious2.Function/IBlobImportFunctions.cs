using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Spurious2.Function
{
    public interface IBlobImportFunctions
    {
        //Task Spirits(Stream myBlob, string name);
        //Task Beer(Stream myBlob, string name);
        Task GetSpiritsPages(string name);
        Task GetBeerPages(string name);
        Task<string> RunOrchestrator(IDurableOrchestrationContext context);
        Task<HttpResponseMessage> HttpStart(HttpRequestMessage req,
            IDurableOrchestrationClient starter);
        Task GetWinePages([ActivityTrigger] string name);
        Task Inventory(Stream myBlob, string name);
        Task LastInventory(Stream myBlob, string name);
        Task LastProduct(Stream myBlob, string name);
        //Task Wine(Stream myBlob, string name);
        //Task Run([TimerTrigger("0 */2 * * * *")] TimerInfo myTimer,
        //    IDurableOrchestrationClient starter);
    }
}
