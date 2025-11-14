using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Spurious2.Infrastructure;

namespace Spurious2.MigrationService;

public class Worker(IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Starting database migration...");
        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<SpuriousContext>();

            await RunMigrationAsync(dbContext, stoppingToken).ConfigureAwait(false);
            //await SeedDataAsync(dbContext, stoppingToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
        Console.WriteLine("Database migration completed.");
    }

    private static async Task RunMigrationAsync(SpuriousContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
            // Run migration in a transaction to avoid partial migration if it fails.
            await dbContext.Database.MigrateAsync(cancellationToken).ConfigureAwait(false)).ConfigureAwait(false);
    }
}
