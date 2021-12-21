using CommandLine;
using LcboWebsiteAdapter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spurious2.Core.LcboImporting.Domain;
using Spurious2.Core.LcboImporting.Services;
using Spurious2.Core.SubdivisionImporting.Domain;
using StructureMap;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LcboScraper
{
    enum Step
    {
        All,
        Stores,
        Products,
        HtmlIntoDb,
        InventoryFromDb,
        StoreVolumes,
        SubdivisionVolumes,
    }

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Late bound")]
    class Options
    {
        [Value(0, MetaName = "Step", Default = Step.All)]
        public Step Step { get; set; }

        [Value(1, MetaName = "Skip", Default = 0)]
        public int Skip { get; set; } = 0;

        [Value(2, MetaName = "Take", Default = 100000)]
        public int Take { get; set; } = 100000;
    }

    class Program
    {
        async static Task Main(string[] args)
        {
            var options = Parser.Default.ParseArguments<Options>(args) as Parsed<Options>;
            Console.WriteLine($"Doing step {options.Value.Step}");
            if (options.Value.Step == Step.All || options.Value.Step == Step.InventoryFromDb)
            {
                Console.WriteLine($"Skipping first {options.Value.Skip} and reading {options.Value.Take}");
            }

            var services = new ServiceCollection()
                    .AddLogging();

            // add StructureMap
            using (var container = new Container())
            {
                container.Configure(config =>
                {
                    // Register stuff in container, using the StructureMap APIs...
                    config.Scan(_ =>
                    {
                        _.AssemblyContainingType(typeof(Program));
                        _.AssemblyContainingType(typeof(ImportingService));
                        _.AssemblyContainingType(typeof(LcboAdapter));
                        _.WithDefaultConventions();
                    });
                    // Populate the container using the service collection
                    config.Populate(services);
                    var configuration = LoadConfiguration();
                    config.For<IConfiguration>().Use(configuration);
                    config.For<IStoreRepository>().Use<Spurious2.SqlRepositories.LcboImporting.Repositories.StoreRepository>();
                    config.For<IProductRepository>().Use<Spurious2.SqlRepositories.LcboImporting.Repositories.ProductRepository>();
                    config.For<IInventoryRepository>().Use<Spurious2.SqlRepositories.LcboImporting.Repositories.InventoryRepository>();
                    config.For<ISubdivisionRepository>().Use<Spurious2.SqlRepositories.SubdivisionImporting.Repositories.SubdivisionRepository>();
                    config.For<SqlConnection>().Use((c) => new SqlConnection(c.GetInstance<IConfiguration>()["SpuriousSqlDb"]));
                    Expression<Func<IContext, Func<SqlConnection>>> sqlConnFactory = (c) => () => new SqlConnection(c.GetInstance<IConfiguration>()["SpuriousSqlDb"]);
                    config.For<Func<SqlConnection>>().Use(sqlConnFactory);
                });

                if (options.Value.Step == Step.All || options.Value.Step == Step.Stores)
                {
                    using (var importer = container.GetInstance<IImportingService>())
                    {
                        var stopwatch = Stopwatch.StartNew();
                        var nodes = await importer.ImportStores().ConfigureAwait(false);
                        stopwatch.Stop();
                        Console.WriteLine("Store count {0}", nodes);
                        Console.WriteLine($"Store import took {stopwatch.Elapsed}");
                    }
                }

                if (options.Value.Step == Step.All || options.Value.Step == Step.Products)
                {
                    using (var importer = container.GetInstance<IImportingService>())
                    {
                        var stopwatch = Stopwatch.StartNew();
                        var nodes = await importer.ImportProducts().ConfigureAwait(false);
                        stopwatch.Stop();
                        Console.WriteLine("Product count {0}", nodes);
                        Console.WriteLine($"Product import took {stopwatch.Elapsed}");
                    }
                }

                if (options.Value.Step == Step.All || options.Value.Step == Step.HtmlIntoDb || options.Value.Step == Step.InventoryFromDb)
                {
                    var invStopwatch = Stopwatch.StartNew();
                    if (options.Value.Step == Step.All || options.Value.Step == Step.HtmlIntoDb)
                    {
                        using (var importer = container.GetInstance<IImportingService>())
                        {
                            await importer.ReadInventoryHtmlsIntoDatabase().ConfigureAwait(false);
                        }
                    }

                    if (options.Value.Step == Step.All || options.Value.Step == Step.InventoryFromDb)
                    {
                        using (var importer = container.GetInstance<IImportingService>())
                        {
                            var invNodes = await importer.UpdateInventoriesFromDatabase(options.Value.Skip, options.Value.Take).ConfigureAwait(false);
                            Console.WriteLine("Inventories count {0}", invNodes);
                        }
                    }

                    invStopwatch.Stop();
                    Console.WriteLine($"Inventory import took {invStopwatch.Elapsed}");
                }

                if (options.Value.Step == Step.All || options.Value.Step == Step.StoreVolumes)
                {
                    // Populate store volume columns
                    using (var storeRepo = container.GetInstance<IStoreRepository>())
                    {
                        var stopwatch = Stopwatch.StartNew();
                        await storeRepo.UpdateStoreVolumes().ConfigureAwait(false);
                        stopwatch.Stop();
                        Console.WriteLine($"Store update took {stopwatch.Elapsed}");
                    }
                }

                if (options.Value.Step == Step.All || options.Value.Step == Step.SubdivisionVolumes)
                {
                    // Update all subdivisions' volumes
                    using (var subdivRepo = container.GetInstance<ISubdivisionRepository>())
                    {
                        var stopwatch = Stopwatch.StartNew();
                        await subdivRepo.UpdateSubdivisionVolumes().ConfigureAwait(false);
                        stopwatch.Stop();
                        Console.WriteLine($"Subdivision update took {stopwatch.Elapsed}");
                    }
                }
            }
        }

        private static IConfiguration LoadConfiguration()
        {
            var devEnvironmentVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
            var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) || devEnvironmentVariable.ToUpperInvariant() == "DEVELOPMENT";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            if (isDevelopment) //only add secrets in development
            {
                builder.AddUserSecrets<Program>();
            }

            return builder.Build();
        }
    }
}
