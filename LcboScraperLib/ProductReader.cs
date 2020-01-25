using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LcboScraperLib
{
    public class ProductReader
    {
        const string baseUrl = "http://www.foodanddrink.ca/lcbo-webapp/";
        public async Task<List<int>> ReadProductIds()
        {
            var result = new List<int>();
            //var hc = new HttpClient();
            string productsString; //= await hc.GetStringAsync($"{baseUrl}productsearch.do?numProducts=15000");
            using (var reader = File.OpenText("Products.xml"))
            {
                productsString = await reader.ReadToEndAsync();
            }

            using (var tr = new StringReader(productsString))
            {
                var serializer = new XmlSerializer(typeof(ProductSummarySearchResponse));
                var productsResponse = serializer.Deserialize(tr) as ProductSummarySearchResponse;
                result.AddRange(productsResponse.Products.Select(p => p.ItemNumber));
            }

            return result;
        }

        public async Task<ProductDetail> ReadProduct(int id)
        {
            var hc = new HttpClient();
            string productsString = await hc.GetStringAsync($"{baseUrl}productdetail.do?itemNumber={id}");
            using (var tr = new StringReader(productsString))
            {
                var serializer = new XmlSerializer(typeof(ProductDetailSearchResponse));
                var productsResponse = serializer.Deserialize(tr) as ProductDetailSearchResponse;
                return productsResponse.Products.Single();
            }
        }

        [XmlRoot("productSearchResponse")]
        public class ProductSummarySearchResponse
        {
            [XmlElement("status")]
            public string Status { get; set; }
            [XmlArray("products")]
            public List<ProductSummary> Products { get; set; }
        }

        [XmlType("product")]
        public class ProductSummary
        {
            [XmlElement("itemNumber")]
            public int ItemNumber { get; set; }
        }

        [XmlRoot("productSearchResponse")]
        public class ProductDetailSearchResponse
        {
            [XmlElement("status")]
            public string Status { get; set; }
            [XmlArray("products")]
            public List<ProductDetail> Products { get; set; }
        }

        [XmlType("product")]
        public class ProductDetail
        {
            [XmlElement("itemNumber")]
            public int ItemNumber { get; set; }

            [XmlElement("itemName")]
            public string ItemName { get; set; }

            [XmlElement("productSize")]
            public string ProductSize { get; set; }

            [XmlElement("liquorType")]
            public string LiquorType { get; set; }

            public override string ToString()
            {
                return $"{ItemNumber} {ItemName} {LiquorType} {ProductSize}";
            }
        }
    }
}
