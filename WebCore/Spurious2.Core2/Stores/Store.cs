using NetTopologySuite.Geometries;
using Spurious2.Core2.Inventories;

namespace Spurious2.Core2.Stores;

public class Store
{
    public int Id { get; set; }

    public string? StoreName { get; set; } = string.Empty;

    public string? City { get; set; } = string.Empty;

    public int? BeerVolume { get; set; }

    public int? WineVolume { get; set; }

    public int? SpiritsVolume { get; set; }

    public Point LocationGeog { get; set; } = Point.Empty;

    public GeoJSON.Text.Geometry.Point Location { get; set; } = new GeoJSON.Text.Geometry.Point();

    public virtual ICollection<Inventory> Inventories { get; private set; } = new List<Inventory>();

    public int? SubdivisionId { get; set; }
}
