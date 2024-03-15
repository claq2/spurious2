using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Spurious2.Core2;
using Spurious2.Infrastructure;

namespace Spurious2.UnitTests;

[TestFixture]
public class DbTests
{
    [Test]
    public async Task Test()
    {
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

        var builder = new ConfigurationBuilder().AddUserSecrets<DbTests>();
        var config = builder.Build();
        var ob = new DbContextOptionsBuilder<SpuriousContext>()
            .UseSqlServer(config.GetConnectionString("SpuriousSqlDb"), b => b.UseNetTopologySuite());
        using var context = new SpuriousContext(ob.Options);
        await context.Database.MigrateAsync().ConfigAwait();
        var mockFactory = new Mock<IDbContextFactory<SpuriousContext>>();
        _ = mockFactory.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(context);
        using var repo = new SpuriousRepository(mockFactory.Object);

        await repo.AddIncomingStoreIds([1, 2, 3]).ConfigAwait();

        context.StoreIncomings.ToList().Count.Should().Be(3);
    }
}
