using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Spurious2.Core.LcboImporting.Adapters;
using Spurious2.Core.LcboImporting.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace LcboWebsiteAdapter
{
    public class LcboAdapter : ILcboAdapter
    {
        const string baseUrl = "http://www.foodanddrink.ca/lcbo-webapp/";
        const string baseInventoryUrl = "http://www.foodanddrink.ca/lcbo-ear/vintages/product/inventory/searchResults.do?language=EN&itemNumber=";
        private readonly Regex storeRegex = new Regex("STORE=([0-9]+)");

        private readonly HttpClientHandler handler;
        private readonly HttpClient hc;

        public LcboAdapter()
        {
            handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            hc = new HttpClient(handler);
            hc.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
        }

        public async Task<Product> ReadProduct(int id)
        {
            var productsString = await hc.GetStringAsync($"{baseUrl}productdetail.do?itemNumber={id}").ConfigureAwait(false);//198069

            using (var tr = new StringReader(productsString))
            {
                using (var xr = XmlReader.Create(tr))
                {
                    var serializer = new XmlSerializer(typeof(ProductSearchResponse));
                    var productSearchResponse = serializer.Deserialize(xr) as ProductSearchResponse;
                    var result = productSearchResponse.Products.Single();
                    return result;
                }
            }
        }

        public Product ReadProductS(int id)
        {
            var productsString = hc.GetStringAsync($"{baseUrl}productdetail.do?itemNumber={id}").Result;//198069

            using (var tr = new StringReader(productsString))
            {
                using (var xr = XmlReader.Create(tr))
                {
                    var serializer = new XmlSerializer(typeof(ProductSearchResponse));
                    var productSearchResponse = serializer.Deserialize(xr) as ProductSearchResponse;
                    var result = productSearchResponse.Products.Single();
                    return result;
                }
            }
        }

        public async Task<IEnumerable<int>> ReadProductIds()
        {
            var productsString = await hc.GetStringAsync($"{baseUrl}productsearch.do?numProducts=15000").ConfigureAwait(false);

            using (var tr = new StringReader(productsString))
            {
                using (var xr = XmlReader.Create(tr))
                {
                    var serializer = new XmlSerializer(typeof(ProductSummarySearchResponse));
                    var productSearchResponse = serializer.Deserialize(xr) as ProductSummarySearchResponse;
                    return productSearchResponse.Products.Select(p => p.ItemNumber);
                }
            }
        }

        public async Task<IEnumerable<Inventory>> ReadProductInventory(int id)
        {
            var result = new List<Inventory>();
            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync($"{baseInventoryUrl}{id}").ConfigureAwait(false);
            var trNodes = doc.DocumentNode.QuerySelectorAll("form[name=\"inventoryresults\"] table[border=\"0\"][width=\"100%\"][cellpadding=\"5\"] tr");
            foreach (var tr in trNodes)
            {
                var tdPNodes = tr.QuerySelectorAll("td p");
                if (tdPNodes.Count() == 4)
                {
                    var storeLinkAttribute = tdPNodes.ElementAt(1).QuerySelector("a").Attributes["href"];
                    var r = storeRegex.Match(storeLinkAttribute.Value);
                    var storeId = Convert.ToInt32(r.Groups[1].Value, CultureInfo.InvariantCulture);
                    var quantity = Convert.ToInt32(tdPNodes.ElementAt(3).InnerText, CultureInfo.InvariantCulture);
                    result.Add(new Inventory { ProductId = id, Quantity = quantity, StoreId = storeId });
                }
            }

            return result.AsEnumerable();
        }

        public string ReadInventoryHtml(int id)
        {
            return hc.GetStringAsync($"{baseInventoryUrl}{id}").Result;
        }

        public async Task<string> ReadInventoryHtmlAsync(int id)
        {
            return await hc.GetStringAsync($"{baseInventoryUrl}{id}").ConfigureAwait(false);
        }

        public List<string> ReadInventoryHtmls(List<int> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var result = new List<string>();
            foreach (var id in ids)
            {
                hc.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
                result.Add(hc.GetStringAsync($"{baseInventoryUrl}{id}").Result);
            }

            return result;
        }

        public IEnumerable<Inventory> ReadProductInventoryS(int id)
        {
            var result = new List<Inventory>();

            var invString = hc.GetStringAsync($"{baseInventoryUrl}{id}").Result;
            var doc = new HtmlDocument();
            doc.LoadHtml(invString);
            var trNodes = doc.DocumentNode.QuerySelectorAll("form[name=\"inventoryresults\"] table[border=\"0\"][width=\"100%\"][cellpadding=\"5\"] tr");
            foreach (var tr in trNodes)
            {
                var tdPNodes = tr.QuerySelectorAll("td p");
                if (tdPNodes.Count() == 4)
                {
                    var storeLinkAttribute = tdPNodes.ElementAt(1).QuerySelector("a").Attributes["href"];
                    var r = storeRegex.Match(storeLinkAttribute.Value);
                    var storeId = Convert.ToInt32(r.Groups[1].Value, CultureInfo.InvariantCulture);
                    var quantity = Convert.ToInt32(tdPNodes.ElementAt(3).InnerText, CultureInfo.InvariantCulture);
                    result.Add(new Inventory { ProductId = id, Quantity = quantity, StoreId = storeId });
                }
            }

            return result.AsEnumerable();
        }

        public async Task<IEnumerable<StoreInfo>> ReadStores()
        {
            var storesString = await hc.GetStringAsync($"{baseUrl}storequery.do?searchType=proximity&longitude=-79.4435649&latitude=43.6581718&numstores=900").ConfigureAwait(false);

            using (var tr = new StringReader(storesString))
            {
                using (var xr = XmlReader.Create(tr))
                {
                    var serializer = new XmlSerializer(typeof(StoreSearchResponse));
                    var storeSearchResponse = serializer.Deserialize(xr) as StoreSearchResponse;
                    return storeSearchResponse.Stores;
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.handler?.Dispose();
                this.hc?.Dispose();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
