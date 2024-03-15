using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Spurious2.Core2;
using Spurious2.Core2.Inventories;
using Spurious2.Infrastructure;

namespace Spurious2.UnitTests;

[TestFixture]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>")]
public class DbTests
{
    private IConfigurationRoot _config;

    [SetUp]
    public async Task Setup()
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
        this._config = builder.Build();
        var ob = new DbContextOptionsBuilder<SpuriousContext>()
            .UseSqlServer(this._config.GetConnectionString("SpuriousSqlDb"),
                b => b.UseNetTopologySuite().MigrationsAssembly("Spurious2"));
        using var context = new SpuriousContext(ob.Options);
        await context.Database.MigrateAsync().ConfigAwait();
        context.StoreIncomings.ExecuteDelete();
    }

    [Test]
    public async Task AddIncomingStores()
    {
        var ob = new DbContextOptionsBuilder<SpuriousContext>()
            .UseSqlServer(this._config.GetConnectionString("SpuriousSqlDb"),
                b => b.UseNetTopologySuite().MigrationsAssembly("Spurious2"));
        using var context = new SpuriousContext(ob.Options);
        var mockFactory = new Mock<IDbContextFactory<SpuriousContext>>();
        mockFactory.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(context);
        using var repo = new SpuriousRepository(mockFactory.Object);

        await repo.AddIncomingStoreIds([1, 2, 3]).ConfigAwait();

        using var context2 = new SpuriousContext(ob.Options);
        var storeIncomings = context2.StoreIncomings.ToList();
        storeIncomings.Count.Should().Be(3);
        storeIncomings[0].Id.Should().Be(1);
        storeIncomings[0].StoreDone.Should().BeFalse();
    }

    [Test]
    public async Task AddIncomingInventories()
    {
        var ob = new DbContextOptionsBuilder<SpuriousContext>()
            .UseSqlServer(this._config.GetConnectionString("SpuriousSqlDb"),
                b => b.UseNetTopologySuite().MigrationsAssembly("Spurious2"));
        using var context = new SpuriousContext(ob.Options);
        var mockFactory = new Mock<IDbContextFactory<SpuriousContext>>();
        mockFactory.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(context);
        using var repo = new SpuriousRepository(mockFactory.Object);

        List<InventoryIncoming> invs = [
            new InventoryIncoming { ProductId = 1, Quantity = 1, StoreId = 1 },
            new InventoryIncoming { ProductId = 1, Quantity = 2, StoreId = 2 },
            new InventoryIncoming { ProductId = 2, Quantity = 3, StoreId = 4 }
        ];

        await repo.AddIncomingInventories(invs).ConfigAwait();

        using var context2 = new SpuriousContext(ob.Options);
        var inventoryIncomings = context2.InventoryIncomings.ToList();
        inventoryIncomings.Count.Should().Be(3);
        inventoryIncomings[0].ProductId.Should().Be(1);
        inventoryIncomings[0].Quantity.Should().Be(1);
        inventoryIncomings[0].StoreId.Should().Be(1);
    }
}
