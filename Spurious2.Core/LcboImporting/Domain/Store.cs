using System;
using System.Collections.Generic;
using GeoAPI.Geometries;
using Spurious2.Core.LcboImporting.Domain;

namespace Spurious2.Core.LcboImporting.Domain
{
    public partial class Store
    {
        public Store()
        {
            Inventories = new HashSet<Inventory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int? BeerVolume { get; set; }
        public int? WineVolume { get; set; }
        public int? SpiritsVolume { get; set; }
        public IGeometry Location { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public virtual ICollection<Inventory> Inventories { get; private set; }
    }
}
