using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Spurious2.Core2;
using Spurious2.Core2.Lcbo;
using Spurious2.Infrastructure;
using Spurious2.Infrastructure.AzureStorage;
using Spurious2.Infrastructure.Lcbo;
using System.Globalization;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        NetTopologySuite.NtsGeometryServices.Instance = new NetTopologySuite.NtsGeometryServices(
               NetTopologySuite.Geometries.Implementation.CoordinateArraySequenceFactory.Instance,
               new NetTopologySuite.Geometries.PrecisionModel(1000d),
               4326 /* ,
            // Note the following arguments are only valid for NTS v2.2
            // Geometry overlay operation function set to use (Legacy or NG)
            NetTopologySuite.Geometries.GeometryOverlay.NG,
            // Coordinate equality comparer to use (CoordinateEqualityComparer or PerOrdinateEqualityComparer)
            new NetTopologySuite.Geometries.CoordinateEqualityComparer() */
           );

        var filepath = string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("WEBSITE_CONTENTSHARE")) ?
                        "log.txt" :
                        @"D:\home\LogFiles\Application\log.txt";

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Worker", LogEventLevel.Warning)
            .MinimumLevel.Override("Host", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Error)
            .MinimumLevel.Override("Function", LogEventLevel.Debug)
            .MinimumLevel.Override("Spurious2.Function2", LogEventLevel.Debug)
            .MinimumLevel.Override("Function2", LogEventLevel.Debug)
            .MinimumLevel.Override("Azure.Storage", LogEventLevel.Error)
            .MinimumLevel.Override("Azure.Core", LogEventLevel.Error)
            .MinimumLevel.Override("Azure.Identity", LogEventLevel.Error)
            .Enrich.FromLogContext()
            .WriteTo.Console(LogEventLevel.Debug, formatProvider: CultureInfo.InvariantCulture
            //, outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level}] [{SourceContext}] {Message}{NewLine}{Exception}{NewLine}"
            )
            .WriteTo.File(filepath, LogEventLevel.Debug, rollingInterval: RollingInterval.Day, formatProvider: CultureInfo.InvariantCulture)
#if DEBUG
            .WriteTo.Seq("http://spurious2.seq:5341", LogEventLevel.Debug)
#endif
            .CreateLogger();
        //services.AddSingleton(Log.Logger);
        //services.AddSingleton<ILoggerProvider>(new Serilog.Extensions.Logging.SerilogLoggerProvider(Log.Logger, dispose: true));
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddLogging(lb => lb.AddSerilog(Log.Logger, true));

        services.AddDbContextFactory<SpuriousContext>((s, opt) => opt.UseSqlServer(
            s.GetRequiredService<IConfiguration>().GetConnectionString("SpuriousSqlDb"),
               b => b.UseNetTopologySuite()
                   .EnableRetryOnFailure()
                   .MigrationsAssembly("Spurious2"))
        );

        services.AddScoped<ISpuriousRepository, SpuriousRepository>();
        services.AddScoped<IImportingService, ImportingService>();
        services.AddScoped<IStorageAdapter, StorageAdapter>();
        services.AddScoped<ILcboAdapter, LcboAdapter>();
        services.AddTransient<LcboHttpClientHandler>();
        services.AddHttpClient<CategorizedProductListClient>()
            .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
        //builder.Services.AddHttpClient<AllProductsListClient>()
        //    .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
        services.AddHttpClient<InventoryClient>()
           .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
        services.AddHttpClient<StoreClient>()
           .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
        services.AddSingleton<Func<string, BlobContainerClient>>((blobContainerName) =>
        {
            //var vars = Environment.GetEnvironmentVariables();
            //var devEnvironmentVariable = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT");
            //var isDevelopment = !string.IsNullOrEmpty(devEnvironmentVariable) && devEnvironmentVariable.ToUpperInvariant() == "DEVELOPMENT";
            BlobClientOptions clientOptions = new();
            clientOptions.Retry.Delay = TimeSpan.FromSeconds(30);
            clientOptions.Retry.MaxRetries = 4;
            //if (isDevelopment)
            //{
            var x = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            return new BlobContainerClient(Environment.GetEnvironmentVariable("AzureWebJobsStorage"), blobContainerName, clientOptions);
            //}
            //else
            //{
            //    var storageUri = new Uri($"{Environment.GetEnvironmentVariable("AzureWebJobsStorage")}/{blobContainerName}");
            //    return new BlobContainerClient(storageUri, new DefaultAzureCredential(), options: clientOptions);
            //}
        });

    })
    .Build();

host.Run();
