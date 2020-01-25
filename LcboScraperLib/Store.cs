using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace LcboScraperLib
{
    public class Store
    {
        [XmlElement("locationNumber")]
        public int Id { get; set; }
        [XmlElement("locationName")]
        public string Name { get; set; }
        [XmlElement("locationCityName")]
        public string City { get; set; }
        [XmlElement("latitude")]
        public float Latitude { get; set; }
        [XmlElement("longtitude")]
        public float Longitude { get; set; }

        public string IdAndDataFieldsAsCsv
        {
            get { return $"{Id},\"{Name}\",{City},{Latitude},{Longitude}"; }
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {4} {2} {3}", Id, Name, Latitude, Longitude, City);
        }
    }
}
