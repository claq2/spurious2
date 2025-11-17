using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Spurious2.Core2;
using Spurious2.Core2.Lcbo;
using Spurious2.Infrastructure;
using Spurious2.Infrastructure.AzureStorage;
using Spurious2.Infrastructure.Lcbo;

var builder = FunctionsApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

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

builder.Logging.Services.Configure<LoggerFilterOptions>(options =>
{
    // The Application Insights SDK adds a default logging filter that instructs ILogger to capture only Warning and more severe logs. Application Insights requires an explicit override.
    // Log levels can also be configured using appsettings.json. For more information, see https://learn.microsoft.com/azure/azure-monitor/app/worker-service#ilogger-logs
    var defaultRule = options.Rules.FirstOrDefault(rule => rule.ProviderName
        == "Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider");
    if (defaultRule is not null)
    {
        options.Rules.Remove(defaultRule);
    }
});

builder.Services.AddDbContextFactory<SpuriousContext>((s, opt) => opt.UseSqlServer(
            s.GetRequiredService<IConfiguration>().GetConnectionString("spuriousdb"),
               b => b.UseNetTopologySuite()
                   .EnableRetryOnFailure()
                   .MigrationsAssembly("Spurious2"))
        );
builder.EnrichSqlServerDbContext<SpuriousContext>();

builder.Services.AddScoped<ISpuriousRepository, SpuriousRepository>();
builder.Services.AddScoped<IImportingService, ImportingService>();
builder.Services.AddScoped<IStorageAdapter, StorageAdapter>();
builder.Services.AddScoped<IQueueAdapter, QueueAdapter>();
builder.Services.AddScoped<ILcboAdapter, LcboAdapter>();
builder.Services.AddTransient<LcboHttpClientHandler>();
builder.Services.AddHttpClient<CategorizedProductListClient>()
    .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
//builder.Services.AddHttpClient<AllProductsListClient>()
//    .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
builder.Services.AddHttpClient<InventoryClient>()
   .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
builder.Services.AddHttpClient<StoreClient>()
   .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
builder.AddAzureBlobServiceClient("blobs", b =>
{
    b.Credential = new DefaultAzureCredential();
}, c => c.ConfigureOptions(o =>
{
    o.Retry.Delay = TimeSpan.FromSeconds(30);
    o.Retry.MaxRetries = 4;
}));
builder.AddAzureQueueServiceClient("queues", b =>
{
    b.Credential = new DefaultAzureCredential();
}, c => c.ConfigureOptions(o =>
{
    o.Retry.Delay = TimeSpan.FromSeconds(30);
    o.Retry.MaxRetries = 4;
}));
//builder.AddAzureBlobContainerClient("blobs", b =>
//{
//    b.Credential = new DefaultAzureCredential();
//}, c => c.ConfigureOptions(o =>
//{
//    o.Retry.Delay = TimeSpan.FromSeconds(30);
//    o.Retry.MaxRetries = 4;
//}));

builder.Services.AddSingleton<Func<string, BlobContainerClient>>(sp =>
{
    var blobServiceClient = sp.GetRequiredService<BlobServiceClient>();
    BlobContainerClient Myfunc(string blobContainerName)
    {
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(blobContainerName);
        return blobContainerClient;
    }

    return Myfunc;
});

builder.Services.AddSingleton<Func<string, QueueClient>>(sp =>
{
    var queueServiceClient = sp.GetRequiredService<QueueServiceClient>();
    QueueClient Myfunc(string queueName)
    {
        var queueClient = queueServiceClient.GetQueueClient(queueName);
        return queueClient;
    }

    return Myfunc;
});

//var host = new HostBuilder()
//    .ConfigureFunctionsWebApplication()
//    .ConfigureServices(services =>
//    {
//        NetTopologySuite.NtsGeometryServices.Instance = new NetTopologySuite.NtsGeometryServices(
//               NetTopologySuite.Geometries.Implementation.CoordinateArraySequenceFactory.Instance,
//               new NetTopologySuite.Geometries.PrecisionModel(1000d),
//               4326 /* ,
//            // Note the following arguments are only valid for NTS v2.2
//            // Geometry overlay operation function set to use (Legacy or NG)
//            NetTopologySuite.Geometries.GeometryOverlay.NG,
//            // Coordinate equality comparer to use (CoordinateEqualityComparer or PerOrdinateEqualityComparer)
//            new NetTopologySuite.Geometries.CoordinateEqualityComparer() */
//           );

//        var filepath = string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("WEBSITE_CONTENTSHARE")) ?
//                        "log.txt" :
//                        @"D:\home\LogFiles\Application\log.txt";

//        Log.Logger = new LoggerConfiguration()
//            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
//            .MinimumLevel.Override("Worker", LogEventLevel.Warning)
//            .MinimumLevel.Override("Host", LogEventLevel.Warning)
//            .MinimumLevel.Override("System", LogEventLevel.Error)
//            .MinimumLevel.Override("Function", LogEventLevel.Debug)
//            .MinimumLevel.Override("Spurious2.Function2", LogEventLevel.Debug)
//            .MinimumLevel.Override("Function2", LogEventLevel.Debug)
//            .MinimumLevel.Override("Azure.Storage", LogEventLevel.Error)
//            .MinimumLevel.Override("Azure.Core", LogEventLevel.Error)
//            .MinimumLevel.Override("Azure.Identity", LogEventLevel.Error)
//            .Enrich.FromLogContext()
//            .WriteTo.Console(LogEventLevel.Debug, formatProvider: CultureInfo.InvariantCulture
//            //, outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level}] [{SourceContext}] {Message}{NewLine}{Exception}{NewLine}"
//            )
//            .WriteTo.File(filepath, LogEventLevel.Debug, rollingInterval: RollingInterval.Day, formatProvider: CultureInfo.InvariantCulture)
//#if DEBUG
//            .WriteTo.Seq("http://spurious2.seq:5341", LogEventLevel.Debug, formatProvider: CultureInfo.InvariantCulture)
//#endif
//            .CreateLogger();
//        //services.AddSingleton(Log.Logger);
//        //services.AddSingleton<ILoggerProvider>(new Serilog.Extensions.Logging.SerilogLoggerProvider(Log.Logger, dispose: true));
//        services.AddApplicationInsightsTelemetryWorkerService();
//        services.ConfigureFunctionsApplicationInsights();
//        services.AddLogging(lb => lb.AddSerilog(Log.Logger, true));

//        services.AddDbContextFactory<SpuriousContext>((s, opt) => opt.UseSqlServer(
//            s.GetRequiredService<IConfiguration>().GetConnectionString("SpuriousSqlDb"),
//               b => b.UseNetTopologySuite()
//                   .EnableRetryOnFailure()
//                   .MigrationsAssembly("Spurious2"))
//        );

//        services.AddScoped<ISpuriousRepository, SpuriousRepository>();
//        services.AddScoped<IImportingService, ImportingService>();
//        services.AddScoped<IStorageAdapter, StorageAdapter>();
//        services.AddScoped<ILcboAdapter, LcboAdapter>();
//        services.AddTransient<LcboHttpClientHandler>();
//        services.AddHttpClient<CategorizedProductListClient>()
//            .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
//        //builder.Services.AddHttpClient<AllProductsListClient>()
//        //    .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
//        services.AddHttpClient<InventoryClient>()
//           .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
//        services.AddHttpClient<StoreClient>()
//           .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
//        services.AddSingleton<Func<string, BlobContainerClient>>((blobContainerName) =>
//        {
//            //var vars = Environment.GetEnvironmentVariables();
//            //var devEnvironmentVariable = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT");
//            //var isDevelopment = !string.IsNullOrEmpty(devEnvironmentVariable) && devEnvironmentVariable.ToUpperInvariant() == "DEVELOPMENT";
//            BlobClientOptions clientOptions = new();
//            clientOptions.Retry.Delay = TimeSpan.FromSeconds(30);
//            clientOptions.Retry.MaxRetries = 4;
//            //if (isDevelopment)
//            //{
//            var x = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
//            return new BlobContainerClient(Environment.GetEnvironmentVariable("AzureWebJobsStorage"), blobContainerName, clientOptions);
//            //}
//            //else
//            //{
//            //    var storageUri = new Uri($"{Environment.GetEnvironmentVariable("AzureWebJobsStorage")}/{blobContainerName}");
//            //    return new BlobContainerClient(storageUri, new DefaultAzureCredential(), options: clientOptions);
//            //}
//        });

//    })
//    .Build();

var host = builder.Build();

host.Run();
