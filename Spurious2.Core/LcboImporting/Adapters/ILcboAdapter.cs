using Spurious2.Core.LcboImporting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spurious2.Core.LcboImporting.Adapters
{
    public interface ILcboAdapter : IDisposable
    {
        Task<string> ReadInventoryHtmlAsync(int id);
        List<string> ReadInventoryHtmls(List<int> ids);
        string ReadInventoryHtml(int id);
        IEnumerable<Inventory> ReadProductInventoryS(int id);
        Product ReadProductS(int id);
        Task<IEnumerable<int>> ReadProductIds();
        Task<IEnumerable<StoreInfo>> ReadStores();

        Task<Product> ReadProduct(int id);

        Task<IEnumerable<Inventory>> ReadProductInventory(int id);
    }
}
