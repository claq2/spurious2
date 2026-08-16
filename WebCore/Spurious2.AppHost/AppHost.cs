using Azure.Provisioning.AppContainers;
using Microsoft.Extensions.Hosting;
using Spurious2.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddConnectionString("spuriousdb");

var storage = builder.AddAzureStorage("storage")
    .RunAsEmulator(az => az.WithDataVolume("spurious-storage"))
;

var blobs = storage.AddBlobs("blobs");
var queues = storage.AddQueues("queues");

var migrations = builder.AddProject<Projects.Spurious2_MigrationService>("spurious2-migrationservice")
    .WithReference(db)
    .WaitFor(db);

var devFrontend = builder.AddJavaScriptApp("spurious2-vite", "../Spurious2/spurious2-vite", "dev")
    ;

builder.AddProject<Projects.Spurious2>("spurious2-webapp")
    .WithReference(db)
    .WaitFor(db)
    .WaitForCompletion(migrations)
    .WithReference(devFrontend)
    .WaitFor(devFrontend)
    .WithHttpHealthCheck("/health")
    .WithHttpsEndpoint()
    .WithExternalHttpEndpoints()
;

var launchProfile = builder.Configuration["DOTNET_LAUNCH_PROFILE"];

if (builder.Environment.IsDevelopment() && launchProfile == "https")
{
    devFrontend.RunWithHttpsDevCertificate("HTTPS_CERT_FILE", "HTTPS_CERT_KEY_FILE");
}

//builder.AddProject<Projects.Spurious2_Processors_Products>("spurious2-processors-products")
//    .WithReference(db)
//    .WithReference(blobs)
//    .WithReference(queues)
//    .WaitFor(db)
//    .WaitFor(blobs)
//    .WaitFor(queues)
//    .WaitForCompletion(migrations);

//builder.AddProject<Projects.Spurious2_Scraper>("spurious2-scraper")
//    .WithReference(db)
//    .WithReference(blobs)
//    .WithReference(queues)
//    .WaitFor(db)
//    .WaitFor(blobs)
//    .WaitFor(queues)
//    .WaitForCompletion(migrations);

//builder.AddProject<Projects.Spurious2_Processors_Inventories>("spurious2-processors-inventories")
//    .WithReference(db)
//    .WithReference(blobs)
//    .WithReference(queues)
//    .WaitFor(db)
//    .WaitFor(blobs)
//    .WaitFor(queues)
//    .WaitForCompletion(migrations);

//builder.AddProject<Projects.Spurious2_Processors_Stores>("spurious2-processors-stores")
//    .WithReference(db)
//    .WithReference(blobs)
//    .WithReference(queues)
//    .WaitFor(db)
//    .WaitFor(blobs)
//    .WaitFor(queues)
//    .WaitForCompletion(migrations);

builder.AddProject<Projects.Scraper>("scraper")
    .PublishAsAzureContainerAppJob((_, j) => j.Configuration.TriggerType = ContainerAppJobTriggerType.Event)
    .WithReference(db)
    .WithReference(blobs)
    .WithReference(queues)
    .WaitFor(db)
    .WaitFor(blobs)
    .WaitFor(queues)
    .WaitForCompletion(migrations);

var productsBuilder = builder.AddProject<Projects.Products>("products")
    .PublishAsAzureContainerAppJob((_, j) =>
    {
        j.Configuration.TriggerType = ContainerAppJobTriggerType.Event;
        j.Configuration.EventTriggerConfig.Parallelism = 3;
        j.Configuration.EventTriggerConfig.ReplicaCompletionCount = 1;
    })
    .WithReference(db)
    .WithReference(blobs)
    .WithReference(queues)
    .WaitFor(db)
    .WaitFor(blobs)
    .WaitFor(queues)
    .WaitForCompletion(migrations);

// Set replica count to 3 for the products job when run locally
if (builder.Environment.IsDevelopment())
{
    productsBuilder.WithReplicas(3);
}

var inventoriesBuilder = builder.AddProject<Projects.Inventories>("inventories")
    .PublishAsAzureContainerAppJob((_, j) =>
    {
        j.Configuration.TriggerType = ContainerAppJobTriggerType.Event;
        j.Configuration.EventTriggerConfig.Parallelism = 3;
        j.Configuration.EventTriggerConfig.ReplicaCompletionCount = 1;
    })
    .WithReference(db)
    .WithReference(blobs)
    .WithReference(queues)
    .WaitFor(db)
    .WaitFor(blobs)
    .WaitFor(queues)
    .WaitForCompletion(migrations);

// Set replica count to 3 for the inventories job when run locally
if (builder.Environment.IsDevelopment())
{
    inventoriesBuilder.WithReplicas(3);
}

builder.AddProject<Projects.Stores>("stores")
    .PublishAsAzureContainerAppJob((_, j) => j.Configuration.TriggerType = ContainerAppJobTriggerType.Event)
    .WithReference(db)
    .WithReference(blobs)
    .WithReference(queues)
    .WaitFor(db)
    .WaitFor(blobs)
    .WaitFor(queues)
    .WaitForCompletion(migrations);

builder.Build().Run();
