using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Spurious2.Core;
using Spurious2.Core.LcboImporting.Adapters;
using Spurious2.Core.LcboImporting.Domain;
using System.Net;
using System.Text;

namespace LcboWebsiteAdapter;

public class LcboAdapterPaged3(CategorizedProductListClient productListClient,
    InventoryClient inventoryClient,
    StoreClient storeClient) : ILcboAdapterPaged2
{
    public List<(Inventory Inventory, Uri Uri)> ExtractInventoriesAndStoreIds(string productId, string contents)
    {
        List<(Inventory, Uri)> result = [];
        HtmlDocument doc = new();
        doc.LoadHtml(contents);
        var table = doc.GetElementbyId("storesTable");
        if (table != null)
        {
            var bodyRows = table.SelectNodes("tbody/tr");
            foreach (HtmlNode row in bodyRows)
            {
                Uri linkToStoreDetails = new("https://example.com");
                Inventory inv = new()
                {
                    ProductId = Convert.ToInt32(productId)
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
                foreach (HtmlNode data in rowDatas)
                {
                    if (data.InnerHtml.Contains("quantity_avail_txt"))
                    {
                        var quantityP = data.SelectSingleNode("p");
                        inv.Quantity = Convert.ToInt32(quantityP.InnerText);
                    }
                    else if (data.InnerHtml.Contains("store_dets_txt"))
                    {
                        var storeA = data.SelectSingleNode("p/a");
                        var storeHref = storeA.Attributes["href"];
                        linkToStoreDetails = new Uri(storeHref.Value);
                        var storeId = storeHref.Value[(storeHref.Value.LastIndexOf('-') + 1)..];
                        inv.StoreId = Convert.ToInt32(storeId);
                    }
                }

                result.Add((inv, linkToStoreDetails));
            }
        }

        return result;
    }

    public async Task<List<(Inventory Inventory, Uri Uri)>> ExtractInventoriesAndStoreIds(string productId, Stream inventoryStream)
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
    public async IAsyncEnumerable<List<Product>> GetCategorizedProducts(ProductType productType)
    {
        var subs = TypesAndSubTypes.ProductsToSubtypeMap[productType];
        foreach (ProductSubtype productSubtype in subs)
        {
            var productsRead = 0;
            var prods = await productListClient.GetProductList(0, productType, productSubtype).ConfigAwait();
            productsRead = prods.results.Length;
            var totalToExpect = prods.totalCountFiltered;
            var productList = prods.results.GetProducts(productType);
            var iterationCount = 0;
            yield return productList;
            iterationCount++;
            while (/*productsRead < totalToExpect &&*/ prods.results.Length > 0)
            {
                prods = await productListClient.GetProductList(productsRead, productType, productSubtype).ConfigAwait();
                productsRead += prods.results.Length;
                productList = prods.results.GetProducts(productType);

                var resultIds = productList.Select(r => r.Id).ToList();

                yield return productList;
                iterationCount++;
            }
        }
    }

    private static readonly char[] separators = ['\r', '\n'];

    public async Task<Store> GetStoreInfo(string storeId, Stream storeStream)
    {
        string contents;
        using var sr = new StreamReader(storeStream, Encoding.UTF8);
        contents = await sr.ReadToEndAsync().ConfigAwait();

        HtmlDocument doc = new();
        doc.LoadHtml(contents);
        var storeNameDiv = doc.DocumentNode.QuerySelector("[data-store-name]");
        var cityDiv = doc.DocumentNode.QuerySelector(".amlocator-text-city");
        var storesArrayScript = doc.DocumentNode.Descendants().Where(n => n.Name == "script"
                && n.InnerText.Contains("function getDistance()")).First().InnerText;
        var splitScript = storesArrayScript.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        var latLine = splitScript.Single(l => l.Contains("var latStore ="));
        var lat = latLine[(latLine.IndexOf('=') + 2)..][..^1]; // Remove space after = and ; at end.
        var longLine = splitScript.Single(l => l.Contains("var lonStore ="));
        var lon = longLine[(longLine.IndexOf('=') + 2)..][..^1]; // Remove space after = and ; at end.
        Store store = new()
        {
            City = WebUtility.HtmlDecode(cityDiv.InnerText[..^2]), // Ends with space and comma
            Name = WebUtility.HtmlDecode(storeNameDiv.Attributes["data-store-name"].Value),
            Id = Convert.ToInt32(storeId),
            Latitude = Convert.ToDecimal(lat),
            Longitude = Convert.ToDecimal(lon),
        };

        return store;
    }

    public async Task<string> GetStorePage(Uri storeUri)
    {
        return await storeClient.GetStorePage(storeUri).ConfigAwait();
    }

    public async Task<string> GetAllStoresInventory(string productId)
    {
        var inventoryPageContents = await inventoryClient.GetInventoryPage(productId).ConfigAwait();
        return inventoryPageContents;
    }
}
