using Azure.Core;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Spurious2.Core2;
using Spurious2.Core2.Lcbo;
using Spurious2.Infrastructure;
using Spurious2.Infrastructure.AzureStorage;
using Spurious2.Infrastructure.Lcbo;

var builder = new HostBuilder();
//builder.UseEnvironment(Environments.Development);
builder.ConfigureWebJobs(b =>
{
    b.AddAzureStorageCoreServices();
    b.AddAzureStorageQueues();
    b.AddAzureStorageBlobs();
}).ConfigureServices((context, s) =>
{
    var isProd = context.HostingEnvironment.IsProduction();

    // Register DefaultAzureCredential — the WebJobs storage extensions pick this up
    s.AddSingleton<TokenCredential>(new DefaultAzureCredential(new DefaultAzureCredentialOptions
    {
        // Always exclude
        ExcludeInteractiveBrowserCredential = true,
        ExcludeBrokerCredential = true,
        ExcludeWorkloadIdentityCredential = true,
        ExcludeEnvironmentCredential = true,
        // Use in prod only
        ExcludeManagedIdentityCredential = !isProd,
        // Use locally only
        ExcludeVisualStudioCodeCredential = isProd,
        ExcludeAzureCliCredential = isProd,
        ExcludeAzureDeveloperCliCredential = isProd,
        ExcludeVisualStudioCredential = isProd,
        ExcludeAzurePowerShellCredential = isProd,
    }));

    s.AddDbContextFactory<SpuriousContext>((s2, opt) => opt
        .UseSqlServer(
            s2.GetRequiredService<IConfiguration>().GetConnectionString("spuriousdb"),
                b => b.UseNetTopologySuite()
                    .EnableRetryOnFailure([2627]) // duplicate key is ok to retry
                    .MigrationsAssembly("Spurious2")
        )
        .LogTo(
            filter: (eventId, level) => eventId.Id == CoreEventId.ExecutionStrategyRetrying,
            logger: (eventData) =>
            {
                var retryEventData = eventData as ExecutionStrategyEventData;
                var exceptions = retryEventData!.ExceptionsEncountered;
                Console.WriteLine($"Retry #{exceptions.Count} with delay {retryEventData.Delay} due to error: {exceptions![exceptions.Count - 1].Message}");
            }
        )
    );
    s.AddScoped<ISpuriousRepository, SpuriousRepository>();
    s.AddScoped<IImportingService, ImportingService>();
    s.AddScoped<IStorageAdapter, StorageAdapter>();
    s.AddScoped<IQueueAdapter, QueueAdapter>();
    s.AddScoped<ILcboAdapter, LcboAdapter>();
    s.AddTransient<LcboHttpClientHandler>();
    s.AddHttpClient<CategorizedProductListClient>()
        .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
    //builder.Services.AddHttpClient<AllProductsListClient>()
    //    .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
    s.AddHttpClient<InventoryClient>()
       .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
    s.AddHttpClient<StoreClient>()
       .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
    s.AddSingleton<Func<string, BlobContainerClient>>(sp =>
    {
        var blobServiceClient = sp.GetRequiredService<BlobServiceClient>();
        BlobContainerClient Myfunc(string blobContainerName)
        {
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(blobContainerName);
            return blobContainerClient;
        }

        return Myfunc;
    });

    s.AddSingleton<Func<string, QueueClient>>(sp =>
    {
        var queueServiceClient = sp.GetRequiredService<QueueServiceClient>();
        QueueClient Myfunc(string queueName)
        {
            var queueClient = queueServiceClient.GetQueueClient(queueName);
            return queueClient;
        }

        return Myfunc;
    });
});

builder.ConfigureLogging((context, b) =>
{
    b.SetMinimumLevel(LogLevel.Error);
    b.AddFilter("Function", LogLevel.Information);
    b.AddFilter("Host", LogLevel.Debug);
    b.AddConsole();
});

var host = builder.Build();
using (host)
{
    await host.RunAsync().ConfigureAwait(false);
}
