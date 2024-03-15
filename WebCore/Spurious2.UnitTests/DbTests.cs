using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Spurious2.Core2;
using Spurious2.Core2.Inventories;
using Spurious2.Core2.Products;
using Spurious2.Core2.Stores;
using Spurious2.Infrastructure;

namespace Spurious2.UnitTests;

[TestFixture]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>")]
public class DbTests
{
    private IConfigurationRoot config;
    private DbContextOptionsBuilder<SpuriousContext> ob;
    private Mock<IDbContextFactory<SpuriousContext>> mockFactory;

    [SetUp]
#pragma warning disable CA1506 // Avoid excessive class coupling
    public async Task Setup()
#pragma warning restore CA1506 // Avoid excessive class coupling
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
        this.config = builder.Build();
        this.ob = new DbContextOptionsBuilder<SpuriousContext>()
            .UseSqlServer(this.config.GetConnectionString("SpuriousSqlDb"),
                b => b.UseNetTopologySuite().MigrationsAssembly("Spurious2"));
        using var context = new SpuriousContext(this.ob.Options);
        await context.Database.MigrateAsync().ConfigAwait();
        await context.StoreIncomings.ExecuteDeleteAsync().ConfigAwait();
        await context.InventoryIncomings.ExecuteDeleteAsync().ConfigAwait();
        await context.ProductIncomings.ExecuteDeleteAsync().ConfigAwait();
        await context.Stores.ExecuteDeleteAsync().ConfigAwait();

        this.mockFactory = new Mock<IDbContextFactory<SpuriousContext>>();
        this.mockFactory
            .Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new SpuriousContext(this.ob.Options));
    }

    [Test]
    public async Task AddIncomingStores()
    {
        using var repo = new SpuriousRepository(this.mockFactory.Object);

        await repo.AddIncomingStoreIds([1, 2, 3]).ConfigAwait();

        using var context2 = new SpuriousContext(this.ob.Options);
        var storeIncomings = context2.StoreIncomings.ToList();
        storeIncomings.Count.Should().Be(3);
        storeIncomings[0].Id.Should().Be(1);
        storeIncomings[0].StoreDone.Should().BeFalse();
    }

    [Test]
    public async Task AddIncomingInventories()
    {
        using var repo = new SpuriousRepository(this.mockFactory.Object);

        List<InventoryIncoming> invs = [
            new InventoryIncoming { ProductId = 1, Quantity = 1, StoreId = 1 },
            new InventoryIncoming { ProductId = 1, Quantity = 2, StoreId = 2 },
            new InventoryIncoming { ProductId = 2, Quantity = 3, StoreId = 4 }
        ];

        await repo.AddIncomingInventories(invs).ConfigAwait();

        using var context2 = new SpuriousContext(this.ob.Options);
        var inventoryIncomings = context2.InventoryIncomings.ToList();
        inventoryIncomings.Count.Should().Be(3);
        inventoryIncomings[0].ProductId.Should().Be(1);
        inventoryIncomings[0].Quantity.Should().Be(1);
        inventoryIncomings[0].StoreId.Should().Be(1);
    }

    [Test]
    public async Task UpdateIncomingStore()
    {
        using var repo = new SpuriousRepository(this.mockFactory.Object);

        await repo.AddIncomingStoreIds([1, 2, 3]).ConfigAwait();

        var store1 = new StoreIncoming { StoreName = "Toronto store 1", Id = 1, City = "Toronto", Latitude = 43.712679m, Longitude = -79.531037m };
        var store2 = new StoreIncoming { StoreName = "Toronto store 2", Id = 2, City = "Toronto-North", Latitude = 44.712679m, Longitude = -78.531037m };
        var store3 = new StoreIncoming { StoreName = "Toronto store 3", Id = 3, City = "Toronto-West", Latitude = 45.712679m, Longitude = -77.531037m };

        await repo.UpdateIncomingStore(store1).ConfigAwait();
        await repo.UpdateIncomingStore(store2).ConfigAwait();
        await repo.UpdateIncomingStore(store3).ConfigAwait();

        using var context2 = new SpuriousContext(this.ob.Options);
        var storeIncomings = context2.StoreIncomings.ToList();
        storeIncomings.Count.Should().Be(3);
        storeIncomings[0].Id.Should().Be(1);
        storeIncomings[0].StoreName.Should().Be("Toronto store 1");
        storeIncomings[0].City.Should().Be("Toronto");
        storeIncomings[0].StoreDone.Should().BeTrue();
        storeIncomings[0].LocationWellKnownText.Should().Be("POINT (-79.531 43.7127)");

        await repo.UpdateStoresFromIncoming().ConfigAwait();

        var stores = context2.Stores.ToList();
        stores.Count.Should().Be(3);
        stores[0].Id.Should().Be(1);
        stores[0].LocationGeog.ToText().Should().Be("POINT (-79.531 43.7127)");
        stores[0].StoreName.Should().Be("Toronto store 1");
        stores[0].City.Should().Be("Toronto");
    }

    [Test]
    public async Task ImportAFewProducts()
    {
        using var repo = new SpuriousRepository(this.mockFactory.Object);

        await repo.AddIncomingStoreIds([1, 2, 3]).ConfigAwait();

        List<ProductIncoming> products = [
                new ProductIncoming
                {
                    Category = "Wine",
                    Id = 1,
                    ProductDone = false, // Repo method sets this to true
                    ProductName = "Red Wine 1",
                    Size = "750",
                    ProductPageUrl = new Uri("https://lcbo.com")
                },
                new ProductIncoming
                {
                    Category = "Beer",
                    Id = 2,
                    ProductDone = false,
                    ProductName = "Beer 1",
                    Size = "6 x 341",
                    ProductPageUrl = new Uri("https://lcbo.com")
                },
                new ProductIncoming
                {
                    Category = "Spirits",
                    Id = 3,
                    ProductDone = false,
                    ProductName = "Spirit 1",
                    Size = "500",
                    ProductPageUrl = new Uri("https://lcbo.com")
                },
            ];

        await repo.ImportAFewProducts(products).ConfigAwait();

        using var context2 = new SpuriousContext(this.ob.Options);

        // ProductIncoming can't be used for reading. Use custom type.
        var productsIncoming = context2.Database.SqlQuery<CustomProductIncoming>($"select * from ProductIncoming").ToList();
        productsIncoming.Count.Should().Be(3);

        productsIncoming[0].Id.Should().Be(1);
        productsIncoming[0].ProductDone.Should().BeTrue();
        productsIncoming[0].Volume.Should().Be(750);
        productsIncoming[0].Category.Should().Be("Wine");
        productsIncoming[0].ProductName.Should().Be("Red Wine 1");

        productsIncoming[1].Volume.Should().Be(2046);
    }
}

public class CustomProductIncoming
{
    public string Category { get; set; }
    public int Id { get; set; }
    public bool ProductDone { get; set; }
    public string ProductName { get; set; }
    public int Volume { get; set; }
}
