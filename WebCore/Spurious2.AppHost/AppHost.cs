using Microsoft.Extensions.Hosting;
using Spurious2.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddConnectionString("spuriousdb");

var storage = builder.AddAzureStorage("storage")
    .RunAsEmulator(az => az.WithLifetime(ContainerLifetime.Persistent))
;

var blobs = storage.AddBlobs("blobs");
var queues = storage.AddQueues("queues");

var migrations = builder.AddProject<Projects.Spurious2_MigrationService>("spurious2-migrationservice")
    .WithReference(db)
    .WaitFor(db)
;

//builder.AddAzureFunctionsProject<Projects.Spurious2_Orchestrator>("spurious2-functions")
//    .WithHostStorage(storage)
//    .WithReference(db)
//    .WithReference(blobs)
//    .WithReference(queues)
//    .WaitFor(db)
//    .WaitFor(storage)
//    .WaitFor(blobs)
//    .WaitFor(queues)
//    .WaitForCompletion(migrations);

var devFrontend = builder.AddNpmApp("spurious2-vite", "../Spurious2/spurious2-vite", "dev")
    ;

builder.AddProject<Projects.Spurious2>("spurious2-webapp")
    .WithReference(db)
    .WaitFor(db)
    .WaitForCompletion(migrations)
    .WithReference(devFrontend)
    .WithHttpHealthCheck("/health")
;

var launchProfile = builder.Configuration["DOTNET_LAUNCH_PROFILE"];

if (builder.Environment.IsDevelopment() && launchProfile == "https")
{
    devFrontend.RunWithHttpsDevCertificate("HTTPS_CERT_FILE", "HTTPS_CERT_KEY_FILE");
}

//builder.AddAzureFunctionsProject<Projects.Spurious2_QueueProcessors_Products>("spurious2-queueprocessors-products")
//    .WithHostStorage(storage)
//    .WithReference(db)
//    .WithReference(blobs)
//    .WithReference(queues)
//    .WaitFor(db)
//    .WaitFor(storage)
//    .WaitFor(blobs)
//    .WaitFor(queues)
//    .WaitForCompletion(migrations);

//builder.AddAzureFunctionsProject<Projects.Spurious2_QueueProcessors_Stores>("spurious2-queueprocessors-stores")
//    .WithHostStorage(storage)
//    .WithReference(db)
//    .WithReference(blobs)
//    .WithReference(queues)
//    .WaitFor(db)
//    .WaitFor(storage)
//    .WaitFor(blobs)
//    .WaitFor(queues)
//    .WaitForCompletion(migrations);

//builder.AddAzureFunctionsProject<Projects.Spurious2_QueueProcessors_Inventories>("spurious2-queueprocessors-inventories")
//    .WithHostStorage(storage)
//    .WithReference(db)
//    .WithReference(blobs)
//    .WithReference(queues)
//    .WaitFor(db)
//    .WaitFor(storage)
//    .WaitFor(blobs)
//    .WaitFor(queues)
//    .WaitForCompletion(migrations);

builder.AddProject<Projects.Spurious2_Processors_Products>("spurious2-processors-products")
    .WithReference(db)
    .WithReference(blobs)
    .WithReference(queues)
    .WaitFor(db)
    .WaitFor(blobs)
    .WaitFor(queues)
    .WaitForCompletion(migrations);

builder.AddProject<Projects.Spurious2_Scraper>("spurious2-scraper")
    .WithReference(db)
    .WithReference(blobs)
    .WithReference(queues)
    .WaitFor(db)
    .WaitFor(blobs)
    .WaitFor(queues)
    .WaitForCompletion(migrations);

builder.AddProject<Projects.Spurious2_Processors_Inventories>("spurious2-processors-inventories")
    .WithReference(db)
    .WithReference(blobs)
    .WithReference(queues)
    .WaitFor(db)
    .WaitFor(blobs)
    .WaitFor(queues)
    .WaitForCompletion(migrations);

builder.AddProject<Projects.Spurious2_Processors_Stores>("spurious2-processors-stores")
    .WithReference(db)
    .WithReference(blobs)
    .WithReference(queues)
    .WaitFor(db)
    .WaitFor(blobs)
    .WaitFor(queues)
    .WaitForCompletion(migrations);

builder.Build().Run();
