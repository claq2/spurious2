using Microsoft.EntityFrameworkCore;
using Spurious2.Core2;
using Spurious2.Core2.Stores;
using Spurious2.Core2.Subdivisions;
using Spurious2.Infrastructure;
using Spurious2.MigrationService;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

var cs = builder.Configuration.GetConnectionString("spuriousdb");
Console.WriteLine($"Using connection string: {cs}");
foreach (var kvp in builder.Configuration.AsEnumerable())
{
    Console.WriteLine($"{kvp.Key}: {kvp.Value}");

}

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

builder.AddSqlServerDbContext<SpuriousContext>("spuriousdb",
    configureDbContextOptions: b => b.UseSqlServer(c =>
    {

        c.MigrationsAssembly("Spurious2")
        .UseNetTopologySuite();
    }));

builder.Services.AddDbContextFactory<SpuriousContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("spuriousdb"),
    b => b.UseNetTopologySuite()
        .EnableRetryOnFailure()
        .MigrationsAssembly("Spurious2")));

builder.Services.AddScoped<ISpuriousRepository, SpuriousRepository>();
builder.Services.AddTransient<IStoreImportingService, StoreImportingService>();
builder.Services.AddTransient<ISubdivisionImportingService, SubdivisionImportingService>();

var host = builder.Build();
host.Run();
