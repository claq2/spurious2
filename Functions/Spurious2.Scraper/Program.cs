using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Spurious2.Core2;
using Spurious2.Core2.Lcbo;
using Spurious2.Infrastructure;
using Spurious2.Infrastructure.AzureStorage;
using Spurious2.Infrastructure.Lcbo;

var builder = new HostBuilder();

NetTopologySuite.NtsGeometryServices.Instance = new NetTopologySuite.NtsGeometryServices(
               NetTopologySuite.Geometries.Implementation.CoordinateArraySequenceFactory.Instance,
               new NetTopologySuite.Geometries.PrecisionModel(1000d),
               4326 /* ,
            // Note the following arguments are only valid for NTS v2.2
            // Geometry overlay operation function set to use (Legacy or NG)
            NetTopologySuite.Geometries.GeometryOverlay.NG,
            // Coordinate equality comparer to use (CoordinateEqualityComparer or PerOrdinateEqualityComparer)
            new NetTopologySuite.Geometries.CoordinateEqualityComparer() */
           );

//builder.ConfigureServices(s =>
//{
//    s.AddDbContextFactory<SpuriousContext>(o =>
//    {
//        o.UseSqlServer(s.Ge)
//    });
//});

//builder.ConfigureServices(s=>s.AddDbContextFactory<SpuriousContext>(opt => opt.UseSqlServer(
//            s.GetRequiredService<IConfiguration>().GetConnectionString("spuriousdb"),
//               b => b.UseNetTopologySuite()
//                   .EnableRetryOnFailure()
//                   .MigrationsAssembly("Spurious2"))
//        );

builder.UseEnvironment(EnvironmentName.Development);
builder.ConfigureWebJobs(b =>
{
    b.AddAzureStorageCoreServices();
    b.AddAzureStorageQueues();
    b.AddAzureStorageBlobs();
    b.Services.AddDbContextFactory<SpuriousContext>((s, opt) => opt.UseSqlServer(
            s.GetRequiredService<IConfiguration>().GetConnectionString("spuriousdb"),
               b => b.UseNetTopologySuite()
                   .EnableRetryOnFailure()
                   .MigrationsAssembly("Spurious2"))
        );
    b.Services.AddScoped<ISpuriousRepository, SpuriousRepository>();
    b.Services.AddScoped<IImportingService, ImportingService>();
    b.Services.AddScoped<IStorageAdapter, StorageAdapter>();
    b.Services.AddScoped<IQueueAdapter, QueueAdapter>();
    b.Services.AddScoped<ILcboAdapter, LcboAdapter>();
    b.Services.AddTransient<LcboHttpClientHandler>();
    b.Services.AddHttpClient<CategorizedProductListClient>()
        .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
    //builder.Services.AddHttpClient<AllProductsListClient>()
    //    .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
    b.Services.AddHttpClient<InventoryClient>()
       .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
    b.Services.AddHttpClient<StoreClient>()
       .ConfigurePrimaryHttpMessageHandler<LcboHttpClientHandler>();
}).ConfigureServices(s =>
{
    //s.Add
});

builder.ConfigureLogging((context, b) =>
{
    b.SetMinimumLevel(LogLevel.Error);
    b.AddFilter("Function", LogLevel.Information);
    b.AddFilter("Host", LogLevel.Debug);
    b.AddConsole();
});

var host = builder.Build();
using (host)
{
    await host.RunAsync().ConfigureAwait(false);
}
