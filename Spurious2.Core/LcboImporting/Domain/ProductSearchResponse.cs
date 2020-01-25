using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Serialization;

namespace Spurious2.Core.LcboImporting.Domain
{
    [XmlRoot("productSearchResponse")]
    public class ProductSearchResponse
    {
        [XmlElement("status")]
        public string Status { get; set; }
        [XmlArray("products")]
        [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO")]
        public List<Product> Products { get; set; }
    }
}
