using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Serialization;

namespace Spurious2.Core.LcboImporting.Domain
{
    [XmlRoot("storeSearchResponse")]
    public class StoreSearchResponse
    {
        [XmlElement("status")]
        public string Status { get; set; }
        [XmlArray("stores")]
        [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO")]
        public List<StoreInfo> Stores { get; set; }
    }
}
