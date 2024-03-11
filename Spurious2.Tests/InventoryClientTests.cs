using FluentAssertions;
using LcboWebsiteAdapter;
using NUnit.Framework;
using System.Net;

namespace Spurious2.Tests;

[TestFixture]
public class InventoryClientTests
{
    [Test]
    public async Task GetThatInventory()
    {
        var client = CreateInventoryClient();
        var html = await client.GetInventoryPage("80127");

        LcboAdapterPaged3 adapter = new(CreateCategorizedProductListClient(),
                                        CreateInventoryClient(),
                                        CreateStoreClient());
        var inventories = adapter.ExtractInventoriesAndStoreIds("80127", html);
        inventories.Count.Should().BeGreaterThanOrEqualTo(600);
        inventories.Should().OnlyContain(i => i.Inventory.ProductId > 0);
        inventories.Should().OnlyContain(i => i.Inventory.Quantity > 0);
        inventories.Should().OnlyContain(i => i.Inventory.StoreId > 0);
        inventories.Should().OnlyContain(i => i.Uri.ToString() != "https://example.com");
    }

    private static InventoryClient CreateInventoryClient()
    {
        HttpClient httpClient = new(new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip
                | DecompressionMethods.Deflate
                | DecompressionMethods.Brotli
        });

        return new InventoryClient(httpClient);
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
