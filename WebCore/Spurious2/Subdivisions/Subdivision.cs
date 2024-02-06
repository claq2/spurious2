using GeoJSON.Text.Geometry;

namespace Spurious2.Subdivisions;

public record Subdivision
{
    public required string Name { get; init; }
    public required int Population { get; init; }
    public required decimal RequestedDensityAmount { get; init; }
    public required Uri BoundaryLink { get; init; }
    public required Point CentreCoordinates { get; init; }
    public required int Id { get; init; }
}
