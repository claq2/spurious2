namespace Spurious2.Infrastructure.All;

public partial class StoreIncoming
{
    public int Id { get; set; }

    public string? City { get; set; }

    public string? StoreName { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public bool StoreDone { get; set; }
}