using Aspire.Hosting.Azure;
using Azure.Provisioning.AppContainers;
using Azure.Provisioning.Expressions;
using Microsoft.Extensions.Hosting;
using Spurious2.AppHost;

var builder = DistributedApplication.CreateBuilder(args);
builder.AddAzureContainerAppEnvironment("spurious-app");

IResourceBuilder<IResourceWithConnectionString> db;

if (builder.ExecutionContext.IsRunMode)
{
    db = builder.AddConnectionString("spuriousdb");
}
else
{
    var sqlServer = builder.AddAzureSqlServer("spuriousdbsql")
         .ConfigureInfrastructure(infra =>
         {
             var resources = infra.GetProvisionableResources();

             var dbRes = resources.OfType<Azure.Provisioning.Sql.SqlDatabase>()
                   .Single();

             dbRes.Sku = new Azure.Provisioning.Sql.SqlSku()
             {
                 Tier = "Basic",
                 Name = "Basic",
                 Capacity = 5,
             };

             dbRes.UseFreeLimit = false;
         });

    db = sqlServer.AddDatabase("spuriousdb");
}

var storage = builder.AddAzureStorage("storage");

if (builder.ExecutionContext.IsRunMode)
{
    storage.RunAsEmulator(az => az.WithDataVolume("spurious-storage"));
}

var blobs = storage.AddBlobs("blobs");
var queues = storage.AddQueues("queues");

var migrations = builder.AddProject<Projects.Spurious2_MigrationService>("spurious2-migrationservice")
    .WithReference(db)
    .WaitFor(db);

var devFrontend = builder.AddJavaScriptApp("spurious2-vite", "../Spurious2/spurious2-vite", "dev")
    .ClearContainerFilesSources()
    .WithContainerFilesSource("./wwwroot/client")
    ;

var webApp = builder.AddProject<Projects.Spurious2>("spurious2-webapp")
    .WithReference(db)
    .WaitFor(db)
    .WaitForCompletion(migrations)
    .WithReference(devFrontend)
    .WaitFor(devFrontend)
    .WithHttpHealthCheck("/health")
    .WithHttpsEndpoint()
    .WithExternalHttpEndpoints()
;

webApp.PublishWithContainerFiles(devFrontend, "./wwwroot/client");

webApp.PublishAsAzureContainerApp((_, app) =>
{
    app.Template.Scale.MinReplicas = 0; // scale to zero
    app.Template.Scale.MaxReplicas = 9; // optional cap
});

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

var scraper = builder.AddProject<Projects.Scraper>("scraper")
    .WithReference(db)
    .WithReference(blobs)
    .WithReference(queues)
    .WaitFor(db)
    .WaitFor(blobs)
    .WaitFor(queues)
    .WaitForCompletion(migrations);

if (builder.ExecutionContext.IsRunMode)
{
    scraper.PublishAsAzureContainerAppJob((_, j) => j.Configuration.TriggerType = ContainerAppJobTriggerType.Event);
}
else
{
    scraper.PublishAsAzureContainerAppJob((_, j) =>
    {
        j.Configuration.TriggerType = ContainerAppJobTriggerType.Manual;
        //j.Configuration.ScheduleTriggerConfig.CronExpression = "0 0 * * *";
    });
}

var productsBuilder = builder.AddProject<Projects.Products>("products")

    .WithReference(db)
    .WithReference(blobs)
    .WithReference(queues)
    .WaitFor(db)
    .WaitFor(blobs)
    .WaitFor(queues)
    .WaitForCompletion(migrations);

productsBuilder.PublishAsAzureContainerAppJob((infra, j) =>
{
    // Get storage account name for queue authentication
    var accountNameParameter = queues.Resource.Parent.NameOutputReference.AsProvisioningParameter(infra);

    // Resolve the identity annotation added to the worker app
    if (!productsBuilder.Resource.TryGetLastAnnotation<AppIdentityAnnotation>(out var identityAnnotation))
    {
        throw new InvalidOperationException("Identity annotation not found.");
    }

    j.Configuration.EventTriggerConfig.Scale.MinExecutions = 0;
    j.Configuration.EventTriggerConfig.Scale.MaxExecutions = 3;

    j.Configuration.TriggerType = ContainerAppJobTriggerType.Event;
    j.Configuration.EventTriggerConfig.Parallelism = 3;
    j.Configuration.EventTriggerConfig.ReplicaCompletionCount = 1;
    j.Configuration.EventTriggerConfig.Scale.PollingIntervalInSeconds = 1;
    j.Configuration.EventTriggerConfig.Scale.Rules.Add(new ContainerAppJobScaleRule
    {
        Name = "products-queue-rule",
        JobScaleRuleType = "azure-queue",
        Metadata = new ObjectExpression(
        // Bicep expressions - referencing other resources dynamically
        new PropertyExpression("accountName", new IdentifierExpression(accountNameParameter.BicepIdentifier)),
        new PropertyExpression("queueName", new StringLiteralExpression("products")),
        new PropertyExpression("queueLength", new IntLiteralExpression(1)) // Start job when 1+ messages
    ),
        Identity = identityAnnotation.IdentityResource.Id.AsProvisioningParameter(infra) // Use managed identity
    });
});

// Set replica count to 3 for the products job when run locally
if (builder.Environment.IsDevelopment())
{
    productsBuilder.WithReplicas(3);
}

var inventoriesBuilder = builder.AddProject<Projects.Inventories>("inventories")
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

inventoriesBuilder.PublishAsAzureContainerAppJob((infra, j) =>
{
    // Get storage account name for queue authentication
    var accountNameParameter = queues.Resource.Parent.NameOutputReference.AsProvisioningParameter(infra);

    // Resolve the identity annotation added to the worker app
    if (!inventoriesBuilder.Resource.TryGetLastAnnotation<AppIdentityAnnotation>(out var identityAnnotation))
    {
        throw new InvalidOperationException("Identity annotation not found.");
    }

    j.Configuration.EventTriggerConfig.Scale.MinExecutions = 0;
    j.Configuration.EventTriggerConfig.Scale.MaxExecutions = 3;

    j.Configuration.TriggerType = ContainerAppJobTriggerType.Event;
    j.Configuration.EventTriggerConfig.Parallelism = 3;
    j.Configuration.EventTriggerConfig.ReplicaCompletionCount = 1;
    j.Configuration.EventTriggerConfig.Scale.PollingIntervalInSeconds = 1;
    j.Configuration.EventTriggerConfig.Scale.Rules.Add(new ContainerAppJobScaleRule
    {
        Name = "inventories-queue-rule",
        JobScaleRuleType = "azure-queue",
        Metadata = new ObjectExpression(
        // Bicep expressions - referencing other resources dynamically
        new PropertyExpression("accountName", new IdentifierExpression(accountNameParameter.BicepIdentifier)),
        new PropertyExpression("queueName", new StringLiteralExpression("inventories")),
        new PropertyExpression("queueLength", new IntLiteralExpression(1)) // Start job when 1+ messages
    ),
        Identity = identityAnnotation.IdentityResource.Id.AsProvisioningParameter(infra) // Use managed identity
    });
});

var storesBuilder = builder.AddProject<Projects.Stores>("stores")
    .WithReference(db)
    .WithReference(blobs)
    .WithReference(queues)
    .WaitFor(db)
    .WaitFor(blobs)
    .WaitFor(queues)
    .WaitForCompletion(migrations);

storesBuilder.PublishAsAzureContainerAppJob((infra, j) =>
{
    // Get storage account name for queue authentication
    var accountNameParameter = queues.Resource.Parent.NameOutputReference.AsProvisioningParameter(infra);

    // Resolve the identity annotation added to the worker app
    if (!storesBuilder.Resource.TryGetLastAnnotation<AppIdentityAnnotation>(out var identityAnnotation))
    {
        throw new InvalidOperationException("Identity annotation not found.");
    }

    j.Configuration.EventTriggerConfig.Scale.MinExecutions = 0;
    j.Configuration.EventTriggerConfig.Scale.MaxExecutions = 3;

    j.Configuration.TriggerType = ContainerAppJobTriggerType.Event;
    j.Configuration.EventTriggerConfig.Parallelism = 1;
    j.Configuration.EventTriggerConfig.ReplicaCompletionCount = 1;
    j.Configuration.EventTriggerConfig.Scale.PollingIntervalInSeconds = 1;
    j.Configuration.EventTriggerConfig.Scale.Rules.Add(new ContainerAppJobScaleRule
    {
        Name = "stores-queue-rule",
        JobScaleRuleType = "azure-queue",
        Metadata = new ObjectExpression(
        // Bicep expressions - referencing other resources dynamically
        new PropertyExpression("accountName", new IdentifierExpression(accountNameParameter.BicepIdentifier)),
        new PropertyExpression("queueName", new StringLiteralExpression("stores")),
        new PropertyExpression("queueLength", new IntLiteralExpression(1)) // Start job when 1+ messages
    ),
        Identity = identityAnnotation.IdentityResource.Id.AsProvisioningParameter(infra) // Use managed identity
    });
});

builder.Build().Run();
