using GeoJSON.Text.Geometry;

namespace Spurious2.Stores;

public record Store
{
    public required int Id { get; init; }
    public required Point LocationCoordinates { get; init; }
    public required string Name { get; init; }
    public required string City { get; init; }

    public required List<Inventory> Inventories { get; init; }
}
