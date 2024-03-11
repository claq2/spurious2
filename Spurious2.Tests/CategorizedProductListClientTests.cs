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
    public async Task Test()
    {
        var client = CreateCategorizedProductListClient();
        var prods = await client.GetProductList(0, Core.LcboImporting.Domain.ProductType.Wine, Core.LcboImporting.Domain.ProductSubtype.Red)
            .ConfigAwait();
        prods.results.Length.Should().Be(9);
        var productList = prods.results.GetProducts(Core.LcboImporting.Domain.ProductType.Wine);
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


