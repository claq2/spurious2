namespace Spurious2.Infrastructure.All;

public partial class ProductIncoming
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public string? Category { get; set; }

    public int? Volume { get; set; }

    public bool ProductDone { get; set; }
}
