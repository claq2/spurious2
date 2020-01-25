using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LcboScraperLib
{
    public class StoreReader
    {
        const string baseUrl = "http://www.foodanddrink.ca/lcbo-webapp/";
        public async Task<List<Store>> ReadStoreIds()
        {
            //var hc = new HttpClient();
            string storesString; // = await hc.GetStringAsync($"{baseUrl}storequery.do?searchType=proximity&longitude=-79.4435649&latitude=43.6581718&numstores=900");
            using (var reader = File.OpenText("Stores.xml"))
            {
                storesString = await reader.ReadToEndAsync();
            }
            
            using (var tr = new StringReader(storesString))
            {
                var serializer = new XmlSerializer(typeof(StoreSearchResponse));
                var storeSearchResponse = serializer.Deserialize(tr) as StoreSearchResponse;
                return storeSearchResponse.Stores;
            }
        }

        [XmlRoot("storeSearchResponse")]
        public class StoreSearchResponse
        {
            [XmlElement("status")]
            public string Status { get; set; }
            [XmlArray("stores")]
            public List<Store> Stores { get; set; }
        }

        [XmlType("store")]
        public class Store
        {
            [XmlElement("locationNumber")]
            public int LocationNumber { get; set; }
            [XmlElement("locationName")]
            public string LocationName { get; set; }
            [XmlElement("locationCityName")]
            public string LocationCityName { get; set; }
            [XmlElement("latitude")]
            public decimal Latitude { get; set; }
            [XmlElement("longitude")]
            public decimal Longitude { get; set; }

            public override string ToString()
            {
                return $"{LocationNumber} {LocationName} {LocationCityName} {Latitude} {Longitude}";
            }
        }
    }
}
