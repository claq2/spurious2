using System;
using System.Collections.Generic;
using GeoAPI.Geometries;
using Spurious2.Core.LcboImporting.Domain;

namespace Spurious2.Core.Reading.Domain
{
    public partial class Store
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Location { get; set; }
        public int BeerVolume { get; set; }
        public int WineVolume { get; set; }
        public int SpiritsVolume { get; set; }
    }
}
