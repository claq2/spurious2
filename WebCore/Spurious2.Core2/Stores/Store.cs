namespace Spurious2.Core2.Stores;

public class Store
{
    public required int Id { get; init; }
    public required string LocationCoordinates { get; init; }
    public required string Name { get; init; }
    public required string City { get; init; }

    public required List<Inventory> Inventories { get; init; }
}
