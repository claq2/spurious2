using Microsoft.EntityFrameworkCore;
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

var host = builder.Build();
host.Run();
