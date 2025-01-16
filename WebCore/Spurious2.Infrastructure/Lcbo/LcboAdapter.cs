using System.Globalization;
using System.Net;
using System.Text;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Spurious2.Core2;
using Spurious2.Core2.Inventories;
using Spurious2.Core2.Lcbo;
using Spurious2.Core2.Products;
using Spurious2.Core2.Stores;

namespace Spurious2.Infrastructure.Lcbo;

public class LcboAdapter(CategorizedProductListClient productListClient,
    InventoryClient inventoryClient,
    StoreClient storeClient) : ILcboAdapter
{
    public IEnumerable<(InventoryIncoming Inventory, Uri Uri)> ExtractInventoriesAndStoreIds(string productId, string contents)
    {
        List<(InventoryIncoming, Uri)> result = [];
        HtmlDocument doc = new();
        doc.LoadHtml(contents);
        var table = doc.GetElementbyId("storesTable");
        if (table != null)
        {
            var bodyRows = table.SelectNodes("tbody/tr");
            foreach (var row in bodyRows)
            {
                Uri linkToStoreDetails = new("https://example.com");
                InventoryIncoming inv = new()
                {
                    ProductId = Convert.ToInt32(productId, CultureInfo.InvariantCulture)
                };

                /*
                <tr>
                    <td>
                        <p class="id_no">619</p>
                    </td>
                    <td>
                        <p class="city_txt">London</p>
                    </td>
                    <td>
                        <p class="name_txt">Sunningdale Rd. &amp; Adelaide St.</p>
                        <p class="address_txt">1820 Adelaide St N</p>
                        <p class="phone_num">(519) 672-9912</p>
                    </td>
                    <td>
                        <p class="quantity_avail_txt">19</p>
                    </td>
                    <td>
                        <p><a title="Store Details" class="store_dets_txt"
                                href="https://www.lcbo.com/en/stores/sunningdale-rd-adelaide-st-762">Store
                                Details</a></p>
                    </td>
                </tr>
                 */

                var rowDatas = row.SelectNodes("td");
                foreach (var data in rowDatas)
                {
                    if (data.InnerHtml.Contains("quantity_avail_txt", StringComparison.Ordinal))
                    {
                        var quantityP = data.SelectSingleNode("p");
                        inv.Quantity = Convert.ToInt32(quantityP.InnerText, CultureInfo.InvariantCulture);
                    }
                    else if (data.InnerHtml.Contains("store_dets_txt", StringComparison.Ordinal))
                    {
                        var storeA = data.SelectSingleNode("p/a");
                        var storeHref = storeA.Attributes["href"];
                        linkToStoreDetails = new Uri(storeHref.Value);
                        var storeId = storeHref.Value[(storeHref.Value.LastIndexOf('-') + 1)..];
                        inv.StoreId = Convert.ToInt32(storeId, CultureInfo.InvariantCulture);
                    }
                }

                result.Add((inv, linkToStoreDetails));
            }
        }

        return result;
    }

    public async Task<IEnumerable<(InventoryIncoming Inventory, Uri Uri)>> ExtractInventoriesAndStoreIds(string productId, Stream inventoryStream)
    {
        string contents;
        using var sr = new StreamReader(inventoryStream, Encoding.UTF8);
        contents = await sr.ReadToEndAsync().ConfigAwait();

        return this.ExtractInventoriesAndStoreIds(productId, contents);
    }

    /// <summary>
    /// Gets the products for the given product type
    /// </summary>
    /// <returns>IAsyncEnumerable<List<Product2>>></returns>
    public async IAsyncEnumerable<IEnumerable<ProductIncoming>> GetCategorizedProducts(ProductType productType)
    {
        var subs = TypesAndSubTypes.ProductsToSubtypeMap[productType];
        foreach (var productSubtype in subs)
        {
            var productsRead = 0;
            var prods = await productListClient.GetProductList(0, productType, productSubtype).ConfigAwait();
            productsRead = prods.results.Count();
            var totalToExpect = prods.totalCountFiltered;
            var productList = prods.results.GetProducts(productType).ToList();
            var iterationCount = 0;
            yield return productList;
            iterationCount++;
            while (/*productsRead < totalToExpect &&*/ productList.Count > 0)
            {
                prods = await productListClient.GetProductList(productsRead, productType, productSubtype).ConfigAwait();
                productsRead += productList.Count;
                productList = prods.results.GetProducts(productType).ToList();

                var resultIds = productList.Select(r => r.Id).ToList();

                yield return productList;
                iterationCount++;
            }
        }
    }

    private static readonly char[] separators = ['\r', '\n'];

    public async Task<StoreIncoming> GetStoreInfo(string storeId, Stream storeStream)
    {
        string contents;
        using var sr = new StreamReader(storeStream, Encoding.UTF8);
        contents = await sr.ReadToEndAsync().ConfigAwait();

        return this.GetStoreInfo(storeId, contents);
    }

    public StoreIncoming GetStoreInfo(string storeId, string contents)
    {
        HtmlDocument doc = new();
        doc.LoadHtml(contents);
        var storeNameDiv = doc.DocumentNode.QuerySelector("[data-store-name]");
        var cityDiv = doc.DocumentNode.QuerySelector(".amlocator-text-city");
        var storesArrayScript = doc.DocumentNode.Descendants().First(n => n.Name == "script"
                && n.InnerText.Contains("function getDistance()", StringComparison.Ordinal)).InnerText;
        var splitScript = storesArrayScript.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        var latLine = splitScript.Single(l => l.Contains("var latStore =", StringComparison.Ordinal));
        var lat = latLine[(latLine.IndexOf('=', StringComparison.Ordinal) + 2)..][..^1]; // Remove space after = and ; at end.
        var longLine = splitScript.Single(l => l.Contains("var lonStore =", StringComparison.Ordinal));
        var lon = longLine[(longLine.IndexOf('=', StringComparison.Ordinal) + 2)..][..^1]; // Remove space after = and ; at end.
        StoreIncoming store = new()
        {
            City = WebUtility.HtmlDecode(cityDiv.InnerText[..^2]), // Ends with space and comma
            StoreName = WebUtility.HtmlDecode(storeNameDiv.Attributes["data-store-name"].Value),
            Id = Convert.ToInt32(storeId, CultureInfo.InvariantCulture),
            Latitude = Convert.ToDecimal(lat, CultureInfo.InvariantCulture),
            Longitude = Convert.ToDecimal(lon, CultureInfo.InvariantCulture),
        };

        return store;
    }

    public Task<string> GetStorePage(Uri storeUri) => storeClient.GetStorePage(storeUri);

    public async Task<string> GetAllStoresInventory(string productId)
    {
        var inventoryPageContents = await inventoryClient.GetInventoryPage(productId).ConfigAwait();
        return inventoryPageContents;
    }
}
