using LcboWebsiteAdapter;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Spurious2.Core;
using Spurious2.Core.LcboImporting.Domain;
using Spurious2.SqlRepositories.LcboImporting.Repositories;
using System.Net;

namespace Spurious2.Tests;

[TestFixture]
public class LcboAdapterPaged3Tests
{
    [Test]
    public async Task GetBeerProducts()
    {
        using (ProductRepository productRepository = new(new("Server=localhost;Database=spurious;User Id=sa;Password=QA@vnet1")))
        {
            using (var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole()))
            {
                var logger = loggerFactory.CreateLogger<LcboAdapterPaged3>();

                LcboAdapterPaged3 adapter = new(CreateCategorizedProductListClient(),
                                                CreateInventoryClient(),
                                                CreateStoreClient());
                List<Product> responses = [];

                await productRepository.ClearIncomingProducts().ConfigAwait();

                await foreach (var s in adapter.GetCategorizedProducts(ProductType.Beer).ConfigAwait())
                {
                    responses.AddRange(s);
                    _ = productRepository.ImportAFew(s).ConfigAwait();
                }

                var ids = responses.Select(r => r.Id).ToList();
                var distinctIds = ids.Distinct().ToList();
                var duplicateIds = ids.Select(i => i).ToList();
                duplicateIds.RemoveAll(id => ids.Count(i => i == id) == 1);
                duplicateIds = duplicateIds.Distinct().ToList();
                var duplicateItems = responses.Where(r => duplicateIds.Contains(r.Id)).ToList();
                duplicateItems = [.. duplicateItems.OrderBy(r => r.Id)];
                Assert.That(ids.Count, Is.EqualTo(ids.Distinct().Count()));
            }
        }
    }

    [Test]
    public async Task GetWineProducts()
    {
        using (ProductRepository productRepository = new(new("Server=localhost;Database=spurious;User Id=sa;Password=QA@vnet1")))
        {
            using (var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole()))
            {
                var logger = loggerFactory.CreateLogger<LcboAdapterPaged3>();

                LcboAdapterPaged3 adapter = new(CreateCategorizedProductListClient(),
                                                CreateInventoryClient(),
                                                CreateStoreClient());
                List<Product> responses = [];

                await productRepository.ClearIncomingProducts().ConfigAwait();

                await foreach (var s in adapter.GetCategorizedProducts(ProductType.Wine).ConfigAwait())
                {
                    responses.AddRange(s);
                    _ = productRepository.ImportAFew(s).ConfigAwait();
                }

                var ids = responses.Select(r => r.Id).ToList();
                var distinctIds = ids.Distinct().ToList();
                var duplicateIds = ids.Select(i => i).ToList();
                duplicateIds.RemoveAll(id => ids.Count(i => i == id) == 1);
                duplicateIds = duplicateIds.Distinct().ToList();
                var duplicateItems = responses.Where(r => duplicateIds.Contains(r.Id)).ToList();
                duplicateItems = [.. duplicateItems.OrderBy(r => r.Id)];
                Assert.That(ids.Count, Is.EqualTo(ids.Distinct().Count()));
            }
        }
    }


    [Test]
    public async Task ParseInventory()
    {
        using (var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole()))
        {
            var logger = loggerFactory.CreateLogger<LcboAdapterPaged3>();

            LcboAdapterPaged3 adapter = new(CreateCategorizedProductListClient(),
                                            CreateInventoryClient(),
                                            CreateStoreClient());
            using var stream = File.OpenRead("80127Inventory.html");
            var inventories = await adapter.ExtractInventoriesAndStoreIds("80127", stream);
            Assert.That(inventories.Count, Is.EqualTo(619));
            Assert.That(inventories.All(i => i.Item1.ProductId > 0));
            Assert.That(inventories.All(i => i.Item1.Quantity > 0));
            Assert.That(inventories.All(i => i.Item1.StoreId > 0));
            Assert.That(inventories.All(i => i.Item2.ToString() != "https://example.com"));
        }
    }

    [Test]
    public async Task ParseStore()
    {
        using (var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole()))
        {
            var logger = loggerFactory.CreateLogger<LcboAdapterPaged3>();

            LcboAdapterPaged3 adapter = new(CreateCategorizedProductListClient(),
                                            CreateInventoryClient(),
                                            CreateStoreClient());
            using var stream = File.OpenRead("store.html");
            var store = await adapter.GetStoreInfo("80127", stream);
            Assert.That(store.Name, Is.EqualTo("Airport & Bovaird"));
            Assert.That(store.City, Is.EqualTo("Brampton"));
            Assert.That(store.Id, Is.EqualTo(80127));
            Assert.That(store.Latitude, Is.EqualTo(43.761869m));
            Assert.That(store.Longitude, Is.EqualTo(-79.721233m));
        }
    }

    private static CategorizedProductListClient CreateCategorizedProductListClient()
    {
        var httpClient = new HttpClient(new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip
                | DecompressionMethods.Deflate
                | DecompressionMethods.Brotli
        });

        return new CategorizedProductListClient(httpClient);
    }


    private static InventoryClient CreateInventoryClient()
    {
        var httpClient = new HttpClient(new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip
                | DecompressionMethods.Deflate
                | DecompressionMethods.Brotli
        });

        return new InventoryClient(httpClient);
    }

    private static StoreClient CreateStoreClient()
    {
        var httpClient = new HttpClient(new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip
                | DecompressionMethods.Deflate
                | DecompressionMethods.Brotli
        });

        return new StoreClient(httpClient);
    }
}
