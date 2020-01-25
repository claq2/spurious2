using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Spurious2.Core.LcboImporting.Domain
{

    [XmlType("store")]
    public class StoreInfo
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
