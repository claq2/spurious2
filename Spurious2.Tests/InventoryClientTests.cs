using HtmlAgilityPack;
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
        var html = await client.GetInventoryPage("103747");
        HtmlDocument doc = new();
        doc.LoadHtml(html);
        var table = doc.GetElementbyId("storesTable");
        var tbody = table.SelectNodes("tbody");
        var rows = tbody[0].SelectNodes("tr");
        foreach (HtmlNode htmlNode in rows)
        {
            var city = htmlNode.SelectNodes("td/p[contains(@class, \"city_txt\")]")
                .Single()
                .InnerText;
            var name = WebUtility.HtmlDecode(htmlNode.SelectNodes("td/p[contains(@class, \"name_txt\")]")
               .Single()
               .InnerText);
            var quantity = htmlNode.SelectNodes("td/p[contains(@class, \"quantity_avail_txt\")]")
               .Single()
               .InnerText;
            var storeDetailsUrl = htmlNode.SelectNodes("td/p/a[contains(@class, \"store_dets_txt\")]")
               .Single()
               .Attributes["href"]
               .Value;
            var storeId = storeDetailsUrl[(storeDetailsUrl.LastIndexOf("-") + 1)..];
        }

        Assert.That(table.InnerText, Does.Contain("Kitchener"));
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
}
