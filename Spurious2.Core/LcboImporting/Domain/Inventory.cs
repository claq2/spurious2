using System;
using System.Collections.Generic;

namespace Spurious2.Core.LcboImporting.Domain
{
    public partial class Inventory
    {
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public int Quantity { get; set; }
    }
}
