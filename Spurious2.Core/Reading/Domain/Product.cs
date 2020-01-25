using Spurious2.Core.LcboImporting.Domain;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Spurious2.Core.Reading.Domain
{
    [XmlType("product")]
    public class Product
    {
        public Product()
        {
        }

        [XmlElement("itemNumber")]
        public int ItemNumber { get; set; }
        [XmlElement("itemName")]
        public string ItemName { get; set; }
        [XmlElement("liquorType")]
        public string LiquorType { get; set; }
        [XmlElement("productSize")]
        public string ProductSize { get; set; }
    }
}
