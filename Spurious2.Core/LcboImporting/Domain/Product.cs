using Spurious2.Core.LcboImporting.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

namespace Spurious2.Core.LcboImporting.Domain
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

        public int PackageVolume
        {
            get
            {
                // 6x341 mL
                // 750 mL
                var productSizeElements = ProductSize.Split(' ');
                var packageHasMultipleContainers = productSizeElements[0].Contains("x");
                var containerElements = packageHasMultipleContainers ? productSizeElements[0].Split('x') : Array.Empty<string>();
                var units = packageHasMultipleContainers ? Convert.ToInt32(containerElements[0], CultureInfo.InvariantCulture) : 1;
                var unitVolume = packageHasMultipleContainers ? Convert.ToInt32(containerElements[1], CultureInfo.InvariantCulture) : Convert.ToInt32(productSizeElements[0], CultureInfo.InvariantCulture);
                return units * unitVolume;
            }
        }
    }
}
