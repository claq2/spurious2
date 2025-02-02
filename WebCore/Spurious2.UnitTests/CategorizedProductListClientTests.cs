using System.Net;
using FluentAssertions;
using Spurious2.Core2;
using Spurious2.Core2.Lcbo;
using Spurious2.Infrastructure.Lcbo;

namespace Spurious2.UnitTests;

[TestFixture]
public class CategorizedProductListClientTests
{
    [Test]
    public async Task GetProductListParses()
    {
        var client = CreateCategorizedProductListClient();
        var prods = await client.GetProductList(0, ProductType.Wine, ProductSubtype.Red)
            .ConfigAwait();
        prods.results.Count().Should().Be(9);
        var productList = prods.results.GetProducts(ProductType.Wine).ToList();
        productList.Count.Should().Be(9);
        productList.Should().OnlyContain(p => p.Id > 0);
        productList.Should().OnlyContain(p => p.Category == "Wine");
        productList.Should().OnlyContain(p => !string.IsNullOrWhiteSpace(p.ProductName));
        productList.Should().OnlyContain(p => p.Volume > 0);
        productList.Should().OnlyContain(p => p.ProductPageUrl.Scheme == "https" && p.ProductPageUrl.DnsSafeHost == "www.lcbo.com");
        productList.Should().OnlyContain(p => p.Size == "750");
        var pageOneIds = productList.Select(p => p.Id);

        // Get page 2
        prods = await client.GetProductList(9, ProductType.Wine, ProductSubtype.Red)
            .ConfigAwait();
        prods.results.Count().Should().Be(9);
        productList = prods.results.GetProducts(ProductType.Wine).ToList();
        productList.Count.Should().Be(9);
        productList.Should().OnlyContain(p => p.Id > 0);
        productList.Should().OnlyContain(p => p.Category == "Wine");
        productList.Should().OnlyContain(p => !string.IsNullOrWhiteSpace(p.ProductName));
        productList.Should().OnlyContain(p => p.Volume > 0);
        productList.Should().OnlyContain(p => p.ProductPageUrl.Scheme == "https" && p.ProductPageUrl.DnsSafeHost == "www.lcbo.com");
        productList.Should().OnlyContain(p => p.Size == "750" || p.Size == "6 x 125");
        var pageTwoIds = productList.Select(p => p.Id);
        var overlappingIds = pageOneIds.Intersect(pageTwoIds).ToList();
        overlappingIds.Count.Should().Be(0);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "<Pending>")]
    private static CategorizedProductListClient CreateCategorizedProductListClient()
    {
        HttpClient httpClient = new(new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip
                | DecompressionMethods.Deflate
                | DecompressionMethods.Brotli,
            CheckCertificateRevocationList = true,
        });

        return new CategorizedProductListClient(httpClient);
    }
}
