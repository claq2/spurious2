using System;
using System.Linq;
using System.Xml.Serialization;

namespace Spurious2.Core.LcboImporting.Domain
{
    [XmlType("product")]
    public class ProductSummary
    {
        [XmlElement("itemNumber")]
        public int ItemNumber { get; set; }
        [XmlElement("itemName")]
        public string ItemName { get; set; }

        public override string ToString()
        {
            return $"{ItemNumber} {ItemName}";
        }
    }
}
