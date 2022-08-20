using Azure.Storage.Blobs;
using LcboWebsiteAdapter;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spurious2.AzureStorageAdapter;
using Spurious2.Core.LcboImporting.Adapters;
using Spurious2.Core.LcboImporting.Domain;
using Spurious2.Core.LcboImporting.Services;
using Spurious2.Core.SubdivisionImporting.Domain;
using System.Data.SqlClient;

[assembly: FunctionsStartup(typeof(Spurious2.Function.Startup))]
namespace Spurious2.Function;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddTransient<IStoreRepository, SqlRepositories.LcboImporting.Repositories.StoreRepository>();
        builder.Services.AddTransient<IProductRepository, SqlRepositories.LcboImporting.Repositories.ProductRepository>();
        builder.Services.AddTransient<IInventoryRepository, SqlRepositories.LcboImporting.Repositories.InventoryRepository>();
        builder.Services.AddTransient<ISubdivisionRepository, SqlRepositories.SubdivisionImporting.Repositories.SubdivisionRepository>();
        builder.Services.AddTransient((c) => new SqlConnection(c.GetService<IConfiguration>()!["SpuriousSqlDb"]));
        Func<SqlConnection> sqlConnFactory(IServiceProvider c) => () => new SqlConnection(c.GetService<IConfiguration>()!["SpuriousSqlDb"]);
        builder.Services.AddTransient(sqlConnFactory);
        builder.Services.AddLogging();
        builder.Services.AddTransient<IImportingService, ImportingService>();
        builder.Services.AddTransient<ILcboAdapterPaged2, LcboAdapterPaged3>();
        builder.Services.AddTransient<IStorageAdapter, StorageAdapter>();
        builder.Services.AddSingleton<Func<string, BlobContainerClient>>((blobContainerName) =>
        {
            //var vars = Environment.GetEnvironmentVariables();
            //var devEnvironmentVariable = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT");
            //var isDevelopment = !string.IsNullOrEmpty(devEnvironmentVariable) && devEnvironmentVariable.ToUpperInvariant() == "DEVELOPMENT";
            BlobClientOptions clientOptions = new();
            clientOptions.Retry.Delay = TimeSpan.FromSeconds(30);
            clientOptions.Retry.MaxRetries = 4;
            //if (isDevelopment)
            //{
            return new BlobContainerClient(Environment.GetEnvironmentVariable("AzureWebJobsStorage"), blobContainerName, clientOptions);
            //}
            //else
            //{
            //    var storageUri = new Uri($"{Environment.GetEnvironmentVariable("AzureWebJobsStorage")}/{blobContainerName}");
            //    return new BlobContainerClient(storageUri, new DefaultAzureCredential(), options: clientOptions);
            //}
        });
        builder.Services.AddTransient<LcboHttpClientHandler>();
        builder.Services.AddHttpClient<CategorizedProductListClient>()
            .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
        //builder.Services.AddHttpClient<AllProductsListClient>()
        //    .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
        builder.Services.AddHttpClient<InventoryClient>()
           .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
        builder.Services.AddHttpClient<StoreClient>()
           .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
    }

    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        base.ConfigureAppConfiguration(builder);

        var devEnvironmentVariable = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT");
        var isDevelopment = !string.IsNullOrEmpty(devEnvironmentVariable) && devEnvironmentVariable.ToUpperInvariant() == "DEVELOPMENT";

        var context = builder.GetContext();

        builder.ConfigurationBuilder
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile(Path.Combine(context.ApplicationRootPath, "local.settings.json"), optional: true, reloadOnChange: true)
            .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        if (isDevelopment) //only add secrets in development
        {
            builder.ConfigurationBuilder.AddUserSecrets<BlobImportFunctions>();
        }
    }
}
