namespace Spurious2.Core2.Subdivisions;

public class Subdivision
{
    public required string Name { get; init; }
    public required int Population { get; init; }
    public required decimal Density { get; init; }
    public required Uri BoundaryLink { get; init; }
    public required string CentreCoordinates { get; init; }
    public required Uri StoresLink { get; init; }
}
