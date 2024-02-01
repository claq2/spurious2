namespace Spurious2.Core.Densities;

public record DensityInfo
{
    public required string ShortName { get; init; }
    public required string Title { get; init; }
    public required Uri Address { get; init; }
}
