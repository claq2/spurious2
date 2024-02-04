using NetTopologySuite.Geometries;
using System.Diagnostics;

namespace Spurious2.Core2.Subdivisions;

[DebuggerDisplay("{SubdivisionName}")]
public class Subdivision
{
    public int Id { get; set; }

    public int Population { get; set; }

    public long BeerVolume { get; set; }

    public long WineVolume { get; set; }

    public long SpiritsVolume { get; set; }

    public string Province { get; set; }

    public string SubdivisionName { get; set; }

    public int AverageIncome { get; set; }

    public int MedianIncome { get; set; }

    public int MedianAfterTaxIncome { get; set; }

    public int AverageAfterTaxIncome { get; set; }

    public decimal? AlcoholDensity { get; set; }

    public decimal? BeerDensity { get; set; }

    public decimal? WineDensity { get; set; }

    public decimal? SpiritsDensity { get; set; }

    public Point GeographicCentreGeog { get; set; }

    public string GeographicCentre { get; set; }

    public Geometry Boundary { get; set; }
}

public enum EndOfDistribution
{
    Top,
    Bottom,
}
