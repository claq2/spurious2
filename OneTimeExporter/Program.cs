// See https://aka.ms/new-console-template for more information

using System.Globalization;
using System.Reflection;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Spurious2.Infrastructure;

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

var configuration = new ConfigurationBuilder()
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .Build();

var serviceProvider = new ServiceCollection()
    .AddLogging(configure =>
    {
        configure.ClearProviders();
        configure.AddConsole();
    })
    .AddDbContext<SpuriousContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("spuriousdb"),
        b => b.UseNetTopologySuite()
            .EnableRetryOnFailure()
            .MigrationsAssembly("Spurious2")))
    .BuildServiceProvider();

var context = serviceProvider.GetRequiredService<SpuriousContext>();
var subdivs = context.Subdivisions.Where(s => s.Province == "Ontario").ToList();

var subdivTextOnly = subdivs.Select(s => new
{
    s.Id,
    s.Population,
    s.SubdivisionName,
    s.AlcoholDensity,
    s.BeerDensity,
    s.Province,
    s.SpiritsDensity,
    s.WineDensity,
    BoundaryWkt = s.Boundary!.ToText(),
    CentreWkt = s.GeographicCentreGeog!.ToText()
}).ToList();

using (var writer = new StreamWriter("cachedsubdivs.csv"))
using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
{
    csv.WriteRecords(subdivTextOnly);
}

var stores = context.Stores.ToList();
var storeTextOnly = stores.Select(s => new
{
    s.Id,
    s.StoreName,
    LocationWkt = s.LocationGeog!.ToText(),
    s.City,
    s.SubdivisionId,
    s.BeerVolume,
    s.WineVolume,
    s.SpiritsVolume,
}).ToList();

using (var writer = new StreamWriter("cachedstores.csv"))
using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
{
    csv.WriteRecords(storeTextOnly);
}

Console.WriteLine("Hello, World!");

