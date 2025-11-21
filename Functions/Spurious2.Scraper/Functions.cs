using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Spurious2.Scraper;

public static class Functions
{
    [NoAutomaticTrigger]
    public static void Start(ILogger logger)
    {
        logger.LogInformation("Starting");
    }

    public static async Task StartByQueue([QueueTrigger("start", Connection = "queues")] string message,
        [Blob("products", FileAccess.Write, Connection = "blobs")] BlobContainerClient bc,
        ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(bc);
        var client = bc.GetBlobClient(message);
        await client.UploadAsync(BinaryData.FromString("started"), overwrite: true).ConfigureAwait(false);
        logger.LogInformation("Starting because of {Message}", message);
    }
}
