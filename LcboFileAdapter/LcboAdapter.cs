using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Spurious2.Core.LcboImporting.Adapters;
using Spurious2.Core.LcboImporting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace LcboFileAdapter
{
    public class LcboAdapter : ILcboAdapter
    {
        private readonly Random random = new Random();
        private readonly Regex storeRegex = new Regex("STORE=([0-9]+)");

        public Task<IEnumerable<StoreInfo>> ReadStores()
        {
            var storeInfos = new List<StoreInfo>();

            using (var xmlReader = XmlReader.Create("Stores.xml"))
            {
                var serializer = new XmlSerializer(typeof(StoreSearchResponse));
                var storeSearchResponse = serializer.Deserialize(xmlReader) as StoreSearchResponse;
                storeInfos.AddRange(storeSearchResponse.Stores);
            }

            return Task.FromResult(storeInfos as IEnumerable<StoreInfo>);
        }

        public IEnumerable<Inventory> ReadProductInventoryS(int id)
        {
            throw new NotImplementedException();
        }

        public Product ReadProductS(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<int>> ReadProductIds()
        {
            var productsIds = new List<int>();

            using (var xmlReader = XmlReader.Create("Products.xml"))
            {
                var serializer = new XmlSerializer(typeof(ProductSummarySearchResponse));
                var productSearchResponse = serializer.Deserialize(xmlReader) as ProductSummarySearchResponse;
                productsIds.AddRange(productSearchResponse.Products.Select(p => p.ItemNumber));
            }

            return Task.FromResult(productsIds as IEnumerable<int>);
        }

        public Task<Product> ReadProduct(int id)
        {
            var use6File = random.Next(2) == 0;
            using (var xmlReader = XmlReader.Create(use6File ? "Product6.xml" : "Product.xml"))
            {
                var serializer = new XmlSerializer(typeof(ProductSearchResponse));
                var productSearchResponse = serializer.Deserialize(xmlReader) as ProductSearchResponse;
                // Put the expected ID to replace the one from the sample file
                var product = productSearchResponse.Products.Single();
                product.ItemNumber = id;
                return Task.FromResult(product);
            }
        }

        public Task<IEnumerable<Inventory>> ReadProductInventory(int id)
        {
            var result = new List<Inventory>();
            var doc = new HtmlDocument();
            doc.Load("VINTAGES - Inventory Position.mhtml");
            var trNodes = doc.DocumentNode.QuerySelectorAll("form[name=\"inventoryresults\"] table[border=\"0\"][width=\"100%\"][cellpadding=\"5\"] tr");
            foreach (var tr in trNodes)
            {
                var tdPNodes = tr.QuerySelectorAll("td p");
                if (tdPNodes.Count() == 4)
                {
                    var storeLinkAttribute = tdPNodes.ElementAt(1).QuerySelector("a").Attributes["href"];
                    var r = storeRegex.Match(storeLinkAttribute.Value);
                    var storeId = Convert.ToInt32(r.Groups[1].Value);
                    var quantity = Convert.ToInt32(tdPNodes.ElementAt(3).InnerText);
                    result.Add(new Inventory { ProductId = id, Quantity = quantity, StoreId = storeId });
                }
            }

            return Task.FromResult(result.AsEnumerable());
        }

        public Task<string> ReadInventoryHtmlAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<string> ReadInventoryHtmls(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public string ReadInventoryHtml(int id)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~LcboAdapter()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
