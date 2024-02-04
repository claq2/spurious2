using NetTopologySuite.Geometries;

namespace Spurious2.Core2.Stores;

public class Store
{
    public int Id { get; set; }

    public string StoreName { get; set; }

    public string City { get; set; }

    public int? BeerVolume { get; set; }

    public int? WineVolume { get; set; }

    public int? SpiritsVolume { get; set; }

    //public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public Point LocationGeog { get; set; }

    public GeoJSON.Text.Geometry.Point Location { get; set; }
}
