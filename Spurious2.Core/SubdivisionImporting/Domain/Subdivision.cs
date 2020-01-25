using System;
using System.Collections.Generic;
using GeoAPI.Geometries;

namespace Spurious2.Core.SubdivisionImporting.Domain
{
    public partial class Subdivision
    {
        public int Id { get; set; }
        public int Population { get; set; }
        public long BeerVolume { get; set; }
        public long WineVolume { get; set; }
        public long SpiritsVolume { get; set; }
        public string Province { get; set; }
        public string Name { get; set; }
        public int AverageIncome { get; set; }
        public int MedianIncome { get; set; }
        public int MedianAfterTaxIncome { get; set; }
        public int AverageAfterTaxIncome { get; set; }
    }
}
