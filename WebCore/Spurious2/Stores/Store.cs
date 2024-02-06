using GeoJSON.Text.Geometry;

namespace Spurious2.Stores;

public class Store
{
    public int Id { get; set; }
    public Point LocationCoordinates { get; set; } = new Point();
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;

    public List<Inventory> Inventories { get; set; } = [];
}
