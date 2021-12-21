using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spurious2.Core.LcboImporting.Domain;
using Spurious2.Core.SubdivisionImporting.Domain;
using Spurious2.Core.SubdivisionImporting.Services;
using StructureMap;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace BoundaryImporter
{
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Late bound")]
    class Options
    {
        [Value(1, MetaName = "BoundaryFile", Default = "D:\\Downloads\\lcsd000a16g_e\\lcsd000a16g_e.gml")]
        public string BoundaryFile { get; set; } = "D:\\Downloads\\lcsd000a16g_e\\lcsd000a16g_e.gml";
    }

    class Program
    {
        static void Main(string[] args)
        {
            var options = Parser.Default.ParseArguments<Options>(args) as Parsed<Options>;

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
                        //_.AssemblyContainingType(typeof(SubdivisionRepository));
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
                });

                using (var importer = container.GetInstance<IImportingService>())
                {
                    var stopwatch = Stopwatch.StartNew();
                    var nodes = 0;
                    if (options.Value.BoundaryFile.EndsWith(".gml", StringComparison.OrdinalIgnoreCase))
                    {
                        nodes = importer.ImportBoundaryFromGmlFile(options.Value.BoundaryFile);
                    }
                    else if (options.Value.BoundaryFile.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                    {
                        nodes = importer.ImportBoundaryFromCsvFile(options.Value.BoundaryFile);
                        //"D:\\Downloads\\lcsd000a16g_e\\subdiv2.csv");
                    }
                    else
                    {
                        throw new ArgumentException("Unknown boundary file extension");
                    }

                    stopwatch.Stop();
                    Console.WriteLine("Node count {0}", nodes);
                    Console.WriteLine($"Import took {stopwatch.Elapsed}");
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
