using Spurious2.Core2.Stores;

namespace Spurious2.Stores;

public record Inventory
{
    public required AlcoholType AlcoholType { get; init; }
    public required decimal Volume { get; init; }
}
