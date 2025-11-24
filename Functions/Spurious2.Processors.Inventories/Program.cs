using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.EntityFrameworkCore;
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
builder.UseEnvironment(Environments.Development);
builder.ConfigureWebJobs(b =>
{
    b.AddAzureStorageCoreServices();
    b.AddAzureStorageQueues();
    b.AddAzureStorageBlobs();

}).ConfigureServices(s =>
{
    s.AddDbContextFactory<SpuriousContext>((s, opt) => opt.UseSqlServer(
            s.GetRequiredService<IConfiguration>().GetConnectionString("spuriousdb"),
               b => b.UseNetTopologySuite()
                   .EnableRetryOnFailure([2627]) // duplicate key is ok to retry
                   .MigrationsAssembly("Spurious2"))
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
