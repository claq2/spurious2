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
}
