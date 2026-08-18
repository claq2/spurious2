using Ardalis.Specification;
using Azure.Core;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Products;
using Spurious2.Core2;
using Spurious2.Core2.Lcbo;
using Spurious2.Core2.Stores;
using Spurious2.Core2.Subdivisions;
using Spurious2.Infrastructure;
using Spurious2.Infrastructure.AzureStorage;
using Spurious2.Infrastructure.Lcbo;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<ProductsWorker>();
builder.AddAzureQueueServiceClient(connectionName: "queues", configureClientBuilder: clientBuilder =>
        clientBuilder.ConfigureOptions(options =>
            options.MessageEncoding = QueueMessageEncoding.Base64));
builder.AddAzureBlobServiceClient(connectionName: "blobs");

builder.Services.AddKeyedSingleton("productsqueuesclient", (sp, s) =>
{
    var queueServiceClient = sp.GetRequiredService<QueueServiceClient>();
    var queueClient = queueServiceClient.GetQueueClient("products");
    queueClient.CreateIfNotExists();
    return queueClient;
});

builder.Services.AddKeyedSingleton("storesqueuesclient", (sp, s) =>
{
    var queueServiceClient = sp.GetRequiredService<QueueServiceClient>();
    var queueClient = queueServiceClient.GetQueueClient("stores");
    queueClient.CreateIfNotExists();
    return queueClient;
});

builder.Services.AddKeyedSingleton("inventoriesqueuesclient", (sp, s) =>
{
    var queueServiceClient = sp.GetRequiredService<QueueServiceClient>();
    var queueClient = queueServiceClient.GetQueueClient("inventories");
    queueClient.CreateIfNotExists();
    return queueClient;
});

builder.Services.AddKeyedSingleton("productsblobsclient", (sp, s) =>
{
    var blobServiceClient = sp.GetRequiredService<BlobServiceClient>();
    var blobContainerClient = blobServiceClient.GetBlobContainerClient("products");
    return blobContainerClient;
});

builder.Services.AddKeyedSingleton("storesblobsclient", (sp, s) =>
{
    var blobServiceClient = sp.GetRequiredService<BlobServiceClient>();
    var blobContainerClient = blobServiceClient.GetBlobContainerClient("stores");
    return blobContainerClient;
});

builder.Services.AddKeyedSingleton("inventoriesblobsclient", (sp, s) =>
{
    var blobServiceClient = sp.GetRequiredService<BlobServiceClient>();
    var blobContainerClient = blobServiceClient.GetBlobContainerClient("inventories");
    return blobContainerClient;
});

builder.Services.AddDbContextFactory<SpuriousContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("spuriousdb"),
        b => b.UseNetTopologySuite()
            .EnableRetryOnFailure()
            .MigrationsAssembly("Spurious2"))
);
builder.EnrichSqlServerDbContext<SpuriousContext>();

var isProd = builder.Environment.IsProduction();

// Register DefaultAzureCredential — the WebJobs storage extensions pick this up
builder.Services.AddSingleton<TokenCredential>(new DefaultAzureCredential(new DefaultAzureCredentialOptions
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

builder.Services.AddScoped<ISpuriousRepository, SpuriousRepository>();
builder.Services.AddScoped<ISubdivisionInMemImportingService, SubdivisionInMemImportingService>();
builder.Services.AddScoped<IStoreInMemImportingService, StoreInMemImportingService>();
builder.Services.AddScoped<InMemoryRepo>();
builder.Services.AddScoped<ISpuriousService, SpuriousService>();
builder.Services.AddScoped(typeof(IReadRepositoryBase<>), typeof(SpuriousSpecRepository<>));
builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(SpuriousSpecRepository<>));
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

var host = builder.Build();
host.Run();
