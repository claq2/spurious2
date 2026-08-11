using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Scraper;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<ScraperWorker>();
builder.Services.AddHostedService<ProductsWorker>();
builder.Services.AddHostedService<InventoriesWorker>();
builder.Services.AddHostedService<StoresWorker>();
builder.AddAzureQueueServiceClient(connectionName: "queues");
builder.AddAzureBlobServiceClient(connectionName: "blobs");

builder.Services.AddKeyedSingleton("productqueuesclient", (sp, s) =>
{
    var queueServiceClient = sp.GetRequiredService<QueueServiceClient>();
    var queueClient = queueServiceClient.GetQueueClient("products");
    return queueClient;
});

builder.Services.AddKeyedSingleton("storesqueuesclient", (sp, s) =>
{
    var queueServiceClient = sp.GetRequiredService<QueueServiceClient>();
    var queueClient = queueServiceClient.GetQueueClient("stores");
    return queueClient;
});

builder.Services.AddKeyedSingleton("inventoryqueuesclient", (sp, s) =>
{
    var queueServiceClient = sp.GetRequiredService<QueueServiceClient>();
    var queueClient = queueServiceClient.GetQueueClient("inventory");
    return queueClient;
});

builder.Services.AddKeyedSingleton("productblobsclient", (sp, s) =>
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

builder.Services.AddKeyedSingleton("inventoryblobsclient", (sp, s) =>
{
    var blobServiceClient = sp.GetRequiredService<BlobServiceClient>();
    var blobContainerClient = blobServiceClient.GetBlobContainerClient("inventory");
    return blobContainerClient;
});

var host = builder.Build();
host.Run();
