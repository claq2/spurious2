using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spurious2.Core.LcboImporting.Domain
{
    public interface IProductRepository : IDisposable
    {
        Task<IEnumerable<int>> GetProductIds();
        Task Import(IEnumerable<Product> products);
    }
}
