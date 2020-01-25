using System;

namespace Spurious2.Web.ServiceModel
{
    public class Subdivision
    {
        public string Name { get; set; }
        public int Population { get; set; }
        public decimal Density { get; set; }
        public Uri BoundaryLink { get; set; }
        public string CentreCoordinates { get; set; }
        public Uri StoresLink { get; set; }
    }
}
