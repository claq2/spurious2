using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Spurious2.Function2
{
    public class Function(ILogger<Function> logger)
    {
        [Function(nameof(Function))]
        public async Task Run([BlobTrigger("samples-workitems/{name}", Connection = "AzureWebJobsStorage")] Stream stream, string name)
        {
            using var blobStreamReader = new StreamReader(stream);
            var content = await blobStreamReader.ReadToEndAsync();
            logger.LogInformation($"C# Blob trigger function Processed blob\n Name: {name} \n Data: {content}");
        }
    }
}
