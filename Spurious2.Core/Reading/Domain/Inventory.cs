using System;
using System.Collections.Generic;

namespace Spurious2.Core.Reading.Domain
{
    public partial class Inventory
    {
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public int Quantity { get; set; }
    }
}
