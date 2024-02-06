using GeoJSON.Text.Geometry;

namespace Spurious2.Subdivisions;

public class Subdivision
{
    public string Name { get; set; } = string.Empty;
    public int Population { get; set; }
    public decimal RequestedDensityAmount { get; set; }
    public Uri BoundaryLink { get; set; } = new Uri("https://blah.com");
    public Point CentreCoordinates { get; set; } = new Point();
    public Uri StoresLink { get; set; } = new Uri("https://blah.com");
    public int Id { get; set; }
}
