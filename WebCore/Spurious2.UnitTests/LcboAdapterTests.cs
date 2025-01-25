using System.Diagnostics.CodeAnalysis;
using System.Net;
using Lcbo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Spurious2.Core2;
using Spurious2.Core2.Lcbo;
using Spurious2.Core2.Products;
using Spurious2.Infrastructure;
using Spurious2.Infrastructure.Lcbo;

namespace Spurious2.UnitTests;

[TestFixture]
[SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>")]
public class LcboAdapterTests
{
    private IConfigurationRoot config;
    private DbContextOptionsBuilder<SpuriousContext> ob;
    private Mock<IDbContextFactory<SpuriousContext>> mockFactory;

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

        var builder = new ConfigurationBuilder().AddUserSecrets<LcboAdapterTests>();
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
    public async Task GetBeerProducts()
    {
        using (SpuriousRepository productRepository = new(this.mockFactory.Object))
        {
            using (var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole()))
            {
                var logger = loggerFactory.CreateLogger<LcboAdapter>();

                LcboAdapter adapter = new(CreateCategorizedProductListClient(),
                                                CreateInventoryClient(),
                                                CreateStoreClient());
                List<ProductIncoming> responses = [];

                await productRepository.ClearIncomingProducts().ConfigAwait();

                await foreach (var s in adapter.GetCategorizedProducts(ProductType.Beer).ConfigAwait())
                {
                    responses.AddRange(s);
                    _ = productRepository.ImportAFewProducts(s).ConfigAwait();
                }

                var ids = responses.Select(r => r.Id).ToList();
                var distinctIds = ids.Distinct().ToList();
                var duplicateIds = ids.Select(i => i).ToList();
                duplicateIds.RemoveAll(id => ids.Count(i => i == id) == 1);
                duplicateIds = duplicateIds.Distinct().ToList();
                var duplicateItems = responses.Where(r => duplicateIds.Contains(r.Id)).ToList();
                duplicateItems = [.. duplicateItems.OrderBy(r => r.Id)];
                Assert.That(ids, Has.Count.EqualTo(ids.Distinct().Count()));
            }
        }
    }

    [Test]
    public async Task GetWineProducts()
    {
        using (SpuriousRepository productRepository = new(this.mockFactory.Object))
        {
            using (var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole()))
            {
                var logger = loggerFactory.CreateLogger<LcboAdapter>();

                LcboAdapter adapter = new(CreateCategorizedProductListClient(),
                                                CreateInventoryClient(),
                                                CreateStoreClient());
                List<ProductIncoming> responses = [];

                await productRepository.ClearIncomingProducts().ConfigAwait();

                await foreach (var s in adapter.GetCategorizedProducts(ProductType.Wine).ConfigAwait())
                {
                    responses.AddRange(s);
                    _ = productRepository.ImportAFewProducts(s).ConfigAwait();
                }

                var ids = responses.Select(r => r.Id).ToList();
                var distinctIds = ids.Distinct().ToList();
                var duplicateIds = ids.Select(i => i).ToList();
                duplicateIds.RemoveAll(id => ids.Count(i => i == id) == 1);
                duplicateIds = duplicateIds.Distinct().ToList();
                var duplicateItems = responses.Where(r => duplicateIds.Contains(r.Id)).ToList();
                duplicateItems = [.. duplicateItems.OrderBy(r => r.Id)];
                Assert.That(ids, Has.Count.EqualTo(ids.Distinct().Count()));
            }
        }
    }

    [Test]
    public async Task ParseInventory()
    {
        using (var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole()))
        {
            var logger = loggerFactory.CreateLogger<LcboAdapter>();

            LcboAdapter adapter = new(CreateCategorizedProductListClient(),
                                            CreateInventoryClient(),
                                            CreateStoreClient());
            using var stream = File.OpenRead("80127Inventory.html");
            var inventories = (await adapter.ExtractInventoriesAndStoreIds("80127", stream).ConfigAwait()).ToList();
            Assert.That(inventories, Has.Count.EqualTo(619));
            Assert.Multiple(() =>
            {
                Assert.That(inventories.All(i => i.Inventory.ProductId > 0));
                Assert.That(inventories.All(i => i.Inventory.Quantity > 0));
                Assert.That(inventories.All(i => i.Inventory.StoreId > 0));
                Assert.That(inventories.All(i => i.Uri.ToString() != "https://example.com"));
            });
        }
    }

    [Test]
    public async Task ParseStore()
    {
        using (var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole()))
        {
            var logger = loggerFactory.CreateLogger<LcboAdapter>();

            LcboAdapter adapter = new(CreateCategorizedProductListClient(),
                                            CreateInventoryClient(),
                                            CreateStoreClient());
            using var stream = File.OpenRead("store.html");
            var store = await adapter.GetStoreInfo("80127", stream).ConfigAwait();
            Assert.Multiple(() =>
            {
                Assert.That(store.StoreName, Is.EqualTo("Airport & Bovaird"));
                Assert.That(store.City, Is.EqualTo("Brampton"));
                Assert.That(store.Id, Is.EqualTo(80127));
                Assert.That(store.Latitude, Is.EqualTo(43.761869m));
                Assert.That(store.Longitude, Is.EqualTo(-79.721233m));
            });
        }
    }

    [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "<Pending>")]
    private static CategorizedProductListClient CreateCategorizedProductListClient()
    {
        var httpClient = new HttpClient(new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip
                | DecompressionMethods.Deflate
                | DecompressionMethods.Brotli,
            CheckCertificateRevocationList = true,
        });

        return new CategorizedProductListClient(httpClient);
    }

    [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "<Pending>")]
    private static InventoryClient CreateInventoryClient()
    {
        var httpClient = new HttpClient(new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip
                | DecompressionMethods.Deflate
                | DecompressionMethods.Brotli,
            CheckCertificateRevocationList = true,
        });

        return new InventoryClient(httpClient);
    }

    [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "<Pending>")]
    private static StoreClient CreateStoreClient()
    {
        var httpClient = new HttpClient(new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip
                | DecompressionMethods.Deflate
                | DecompressionMethods.Brotli,
            CheckCertificateRevocationList = true,
        });

        return new StoreClient(httpClient);
    }
}
