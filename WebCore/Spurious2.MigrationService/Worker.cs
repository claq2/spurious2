using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Spurious2.Core2;
using Spurious2.Core2.Stores;
using Spurious2.Core2.Subdivisions;
using Spurious2.Infrastructure;

namespace Spurious2.MigrationService;

public class Worker(IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Starting database migration...");
        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<SpuriousContext>();

            await RunMigrationAsync(dbContext, stoppingToken).ConfigureAwait(false);
            await this.SeedDataAsync(dbContext, stoppingToken).ConfigureAwait(false);
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
        await strategy.ExecuteAsync(async (ct) =>
            // Run migration in a transaction to avoid partial migration if it fails.
            dbContext.Database.MigrateAsync(ct), cancellationToken).ConfigureAwait(false);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
    private async Task SeedDataAsync(SpuriousContext dbContext, CancellationToken cancellationToken)
    {
        var sw = new Stopwatch();
        sw.Start();
        var strategy = dbContext.Database.CreateExecutionStrategy();
        using var scope = serviceProvider.CreateScope();
        var subdivsWithBoundary = await dbContext.Subdivisions.CountAsync(sd => sd.Boundary != null, cancellationToken).ConfigureAwait(false);
        if (subdivsWithBoundary < 5161)
        {
            var subdivisionImportingService = scope.ServiceProvider.GetRequiredService<ISubdivisionImportingService>();
            // add from boundary file
            await subdivisionImportingService.ImportBoundaryFromCsvFile("subdiv.csv").ConfigureAwait(false);
        }

        var subdivsWithPopulation = await dbContext.Subdivisions.CountAsync(sd => sd.Population > 0, cancellationToken).ConfigAwait();
        if (subdivsWithPopulation < 4830)
        {
            // add from population file
            var subDivImporter = scope.ServiceProvider.GetRequiredService<ISubdivisionImportingService>();
            await subDivImporter.ImportPopulationFrom98File("population.csv").ConfigureAwait(false);
        }

        //using (var scope = serviceProvider.CreateScope())
        //{
        //await strategy.ExecuteAsync(async (ct) =>
        //{
        //    using (var scopeX = serviceProvider.CreateScope())
        //    {
        //        var importTasks = new List<Task>();
        //        var subdivsWithBoundary = await dbContext.Subdivisions.CountAsync(sd => sd.Boundary != null, ct).ConfigureAwait(false);
        //        if (subdivsWithBoundary < 5161)
        //        {
        //            var subdivisionImportingService = scopeX.ServiceProvider.GetRequiredService<ISubdivisionImportingService>();
        //            // add from boundary file
        //            importTasks.Add(subdivisionImportingService.ImportBoundaryFromCsvFile("subdiv.csv"));
        //        }

        //        var subdivsWithPopulation = await dbContext.Subdivisions.CountAsync(sd => sd.Population > 0, ct).ConfigAwait();
        //        if (subdivsWithPopulation < 4830)
        //        {
        //            // add from population file
        //            var subDivImporter = scopeX.ServiceProvider.GetRequiredService<ISubdivisionImportingService>();
        //            importTasks.Add(subDivImporter.ImportPopulationFrom98File("population.csv"));
        //        }

        //        await Task.WhenAll(importTasks).ConfigAwait();
        //    }
        //}, cancellationToken).ConfigureAwait(false);





        Console.WriteLine("Took {0} to import subdiv data", sw.Elapsed);

        var storeCount = await dbContext.Stores.CountAsync(cancellationToken).ConfigAwait();
        if (storeCount < 653)
        {
            var storeImporter = scope.ServiceProvider.GetRequiredService<IStoreImportingService>();
            await storeImporter.ImportStoresFromCsvFile("stores.csv").ConfigAwait();
        }

        //await strategy.ExecuteAsync(async (ct) =>
        //{
        //    using (var scopeY = serviceProvider.CreateScope())
        //    {
        //        var storeCount = await dbContext.Stores.CountAsync(ct).ConfigAwait();
        //        if (storeCount < 653)
        //        {
        //            var storeImporter = scopeY.ServiceProvider.GetRequiredService<IStoreImportingService>();
        //            await storeImporter.ImportStoresFromCsvFile("stores.csv").ConfigAwait();
        //        }
        //    }
        //}, cancellationToken).ConfigureAwait(false);
        //}

        sw.Stop();
        Console.WriteLine("Took {0} to set up DB", sw.Elapsed);
    }
}
