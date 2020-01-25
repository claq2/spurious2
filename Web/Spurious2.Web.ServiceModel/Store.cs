using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Spurious2.Web.ServiceModel
{
    public class Store
    {
        public int Id { get; set; }
        public string LocationCoordinates { get; set; }
        public string Name { get; set; }

        [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "<Pending>")]
        public List<Inventory> Inventories { get; set; }
    }
}
