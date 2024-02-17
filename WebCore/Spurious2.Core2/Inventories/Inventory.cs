using Spurious2.Core.Products;
using Spurious2.Core2.Stores;

namespace Spurious2.Core.Inventories;

public partial class Inventory
{
    public int ProductId { get; set; }

    public int StoreId { get; set; }

    public int Quantity { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
