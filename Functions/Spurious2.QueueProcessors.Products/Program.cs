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
    o.MessageEncoding = QueueMessageEncoding.Base64;
}));

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

builder.Build().Run();
