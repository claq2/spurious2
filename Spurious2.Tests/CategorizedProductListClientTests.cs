using FluentAssertions;
using LcboWebsiteAdapter;
using NUnit.Framework;
using Spurious2.Core;
using System.Net;

namespace Spurious2.Tests;

[TestFixture]
public class CategorizedProductListClientTests
{
    [Test]
    public async Task GetProductListParses()
    {
        var client = CreateCategorizedProductListClient();
        var prods = await client.GetProductList(0, Core.LcboImporting.Domain.ProductType.Wine, Core.LcboImporting.Domain.ProductSubtype.Red)
            .ConfigAwait();
        prods.results.Length.Should().Be(9);
        var productList = prods.results.GetProducts(Core.LcboImporting.Domain.ProductType.Wine);
        productList.Count.Should().Be(9);
        productList.Should().OnlyContain(p => p.Id > 0);
        productList.Should().OnlyContain(p => p.LiquorType == "Wine");
        productList.Should().OnlyContain(p => !string.IsNullOrWhiteSpace(p.Name));
        productList.Should().OnlyContain(p => p.PackageVolume > 0);
        productList.Should().OnlyContain(p => p.ProductPageUrl.Scheme == "https" && p.ProductPageUrl.DnsSafeHost == "www.lcbo.com");
        productList.Should().OnlyContain(p => p.Size == "750");
        var pageOneIds = productList.Select(p => p.Id);

        // Get page 2
        prods = await client.GetProductList(9, Core.LcboImporting.Domain.ProductType.Wine, Core.LcboImporting.Domain.ProductSubtype.Red)
            .ConfigAwait();
        prods.results.Length.Should().Be(9);
        productList = prods.results.GetProducts(Core.LcboImporting.Domain.ProductType.Wine);
        productList.Count.Should().Be(9);
        productList.Should().OnlyContain(p => p.Id > 0);
        productList.Should().OnlyContain(p => p.LiquorType == "Wine");
        productList.Should().OnlyContain(p => !string.IsNullOrWhiteSpace(p.Name));
        productList.Should().OnlyContain(p => p.PackageVolume > 0);
        productList.Should().OnlyContain(p => p.ProductPageUrl.Scheme == "https" && p.ProductPageUrl.DnsSafeHost == "www.lcbo.com");
        productList.Should().OnlyContain(p => p.Size == "750");
        var pageTwoIds = productList.Select(p => p.Id);
        var overlappingIds = pageOneIds.Intersect(pageTwoIds).ToList();
        overlappingIds.Count.Should().Be(0);
    }

    private static CategorizedProductListClient CreateCategorizedProductListClient()
    {
        HttpClient httpClient = new(new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip
                | DecompressionMethods.Deflate
                | DecompressionMethods.Brotli
        });

        return new CategorizedProductListClient(httpClient);
    }
}


