using NetTopologySuite.Geometries;

namespace Spurious2.Core2.Stores;

public partial class StoreIncoming
{
    public int Id { get; set; }
    public string? City { get; set; }
    public string? StoreName { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public bool StoreDone { get; set; }
    public string? LocationWellKnownText { get; set; }
    public int? BeerVolume { get; set; }
    public int? WineVolume { get; set; }
    public int? SpiritsVolume { get; set; }
    public Point LocationGeog { get; set; } = Point.Empty;

    public override string ToString() => $"{this.Id} {this.StoreName} {this.City}";
}
