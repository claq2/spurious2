using Microsoft.Extensions.Hosting;
using Spurious2.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddConnectionString("spuriousdb");

var storage = builder.AddAzureStorage("storage")
    .RunAsEmulator(az => az.WithLifetime(ContainerLifetime.Persistent))
;

var blobs = storage.AddBlobs("blobs");

var migrations = builder.AddProject<Projects.Spurious2_MigrationService>("spurious2-migrationservice")
    .WithReference(db)
    .WaitFor(db)
;

var functions = builder.AddAzureFunctionsProject<Projects.Spurious2_Function2>("functions")
    .WithHostStorage(storage)
    .WithReference(db)
    .WithReference(blobs)
    .WaitFor(db)
    .WaitFor(storage)
    .WaitFor(blobs)
    .WaitForCompletion(migrations);

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

builder.AddAzureFunctionsProject<Projects.FunctionApp1>("functionapp1");

builder.Build().Run();
