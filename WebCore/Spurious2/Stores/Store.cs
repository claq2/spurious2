using GeoJSON.Text.Geometry;

namespace Spurious2.Stores;

public class Store
{
    public int Id { get; set; }
    public Point LocationCoordinates { get; set; }
    public string Name { get; set; }
    public string City { get; set; }

    public List<Inventory> Inventories { get; set; }
}
