using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;
using FluentAssertions;
using Lcbo;
using Spurious2.Core2;

namespace Spurious2.UnitTests;

[TestFixture]
[SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>")]
public class InventoryClientTests
{
    [Test]
    public async Task GetThatInventoryAndStores()
    {
        var client = CreateInventoryClient();
        var html = await client.GetInventoryPage("80127").ConfigAwait();

        LcboAdapter adapter = new(CreateCategorizedProductListClient(),
                                        CreateInventoryClient(),
                                        CreateStoreClient());
        var inventories = adapter.ExtractInventoriesAndStoreIds("80127", html);
        inventories.Count.Should().BeGreaterThanOrEqualTo(600);
        inventories.Should().OnlyContain(i => i.Inventory.ProductId > 0);
        inventories.Should().OnlyContain(i => i.Inventory.Quantity > 0);
        inventories.Should().OnlyContain(i => i.Inventory.StoreId > 0);
        inventories.Should().OnlyContain(i => i.Uri.DnsSafeHost == "www.lcbo.com");

        var inventory = inventories.Single(i => i.Uri.ToString().EndsWith("-1", StringComparison.Ordinal));

        var storeHtml = await adapter.GetStorePage(inventory.Uri).ConfigAwait();
        var storeInfo = adapter.GetStoreInfo(inventory.Inventory.StoreId.ToString(CultureInfo.InvariantCulture),
            storeHtml);
        storeInfo.Id.Should().Be(1);
        storeInfo.StoreName.Should().Be("Hwy 401 & Weston (Crossroads)");
        storeInfo.City.Should().Be("Toronto-North York");
        storeInfo.Latitude.Should().Be(43.712679m);
        storeInfo.Longitude.Should().Be(-79.531037m);
    }

    [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "<Pending>")]
    private static InventoryClient CreateInventoryClient()
    {
        HttpClient httpClient = new(new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip
                | DecompressionMethods.Deflate
                | DecompressionMethods.Brotli,
            CheckCertificateRevocationList = true,
        });

        return new InventoryClient(httpClient);
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
