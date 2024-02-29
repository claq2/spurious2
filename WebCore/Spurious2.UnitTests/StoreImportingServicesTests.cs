using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Spurious2.Core2.Stores;
using Spurious2.Infrastructure;
using Spurious2.Infrastructure.All;

namespace Spurious2.UnitTests;

[TestFixture]
public class StoreImportingServicesTests
{
    private readonly IConfiguration configuration;

    public StoreImportingServicesTests()
    {
        NetTopologySuite.NtsGeometryServices.Instance = new NetTopologySuite.NtsGeometryServices(
               NetTopologySuite.Geometries.Implementation.CoordinateArraySequenceFactory.Instance,
               new NetTopologySuite.Geometries.PrecisionModel(1000d),
               4326);

        this.configuration = new ConfigurationBuilder()
                    .AddUserSecrets<StoreImportingServicesTests>()
                    .Build();
    }

    [Test]
    public void Test()
    {
        var opt = new DbContextOptionsBuilder<SpuriousContext>();
        _ = opt.UseSqlServer(this.configuration.GetConnectionString("SpuriousSqlDb"), b => b.UseNetTopologySuite());
        using var context = new SpuriousContext(opt.Options);
        using var repo = new SpuriousRepository(context);
        using var svc = new StoreImportingService(repo);

        var stores = svc.ImportStoresFromCsvFile("stores.csv").ToList();
        _ = stores.Count.Should().Be(653);
    }
}
