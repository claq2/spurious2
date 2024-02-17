using Spurious2.Core.Inventories;

namespace Spurious2.Core.Products;

public partial class Product
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public string? Category { get; set; }

    public int? Volume { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
