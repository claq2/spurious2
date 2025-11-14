var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddConnectionString("spuriousdb");

var storage = builder.AddAzureStorage("storage")
                     .RunAsEmulator(az => az.WithLifetime(ContainerLifetime.Persistent))
                     ;

var blobs = storage.AddBlobs("blobs");

var functions = builder.AddAzureFunctionsProject<Projects.Spurious2_Function2>("functions")
                       .WithHostStorage(storage)
                       .WithReference(db)
                       .WithReference(blobs)
                       .WaitFor(db)
                       .WaitFor(storage)
                       .WaitFor(blobs);

builder.Build().Run();
