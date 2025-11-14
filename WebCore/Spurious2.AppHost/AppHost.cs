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
                       .WaitFor(migrations);


builder.Build().Run();
