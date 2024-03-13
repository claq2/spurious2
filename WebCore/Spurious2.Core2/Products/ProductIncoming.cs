namespace Spurious2.Core2.Products;

public partial class ProductIncoming
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public string? Category { get; set; }

    public int? Volume { get; set; }

    public bool ProductDone { get; set; }
}
