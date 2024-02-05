using NetTopologySuite.Geometries;
using System.Diagnostics;

namespace Spurious2.Core2.Subdivisions;

[DebuggerDisplay("{DebugDisplay,nq}")]
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

    public decimal RequestedDensityAmount { get; set; }

    public Point GeographicCentreGeog { get; set; }

    public GeoJSON.Text.Geometry.Point GeographicCentre { get; set; }

    public Geometry Boundary { get; set; }

    private string DebugDisplay {
        get { return $"{SubdivisionName} All {AlcoholDensity:n} Beer {BeerDensity:n}"; }
    }
}

public enum EndOfDistribution
{
    Top,
    Bottom,
}
