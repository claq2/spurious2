using Spurious2.Core2.Inventories;

namespace Spurious2.Core2.Products;

public partial class Product
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public string? Category { get; set; }

    public int? Volume { get; set; }

    public virtual ICollection<Inventory> Inventories { get; private set; } = new List<Inventory>();
}
