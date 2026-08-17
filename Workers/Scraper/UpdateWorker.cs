using Spurious2.Core2;
using Spurious2.Core2.Lcbo;

namespace Scraper;

public class UpdateWorker(IServiceScopeFactory serviceScopeFactory,
    ILogger<UpdateWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Wait 60 seconds to let scraper get started
        await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken).ConfigureAwait(false);

        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var importingService = scope.ServiceProvider.GetRequiredService<IImportingService>();
            if (await importingService.AreAnyIncomingRecordsNotDone(stoppingToken).ConfigAwait())
            {
                logger.LogInformation("Updating all");
                await importingService.UpdateAll(stoppingToken).ConfigureAwait(false);
            }
            else
            {
                // Wait 30 seconds and check again
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken).ConfigureAwait(false);
            }
        }
    }
}
