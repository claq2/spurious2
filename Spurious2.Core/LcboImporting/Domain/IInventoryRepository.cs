using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spurious2.Core.LcboImporting.Domain
{
    public interface IInventoryRepository : IDisposable
    {
        Task UpdateInventoriesFromIncoming();
        Task ClearIncomingInventory();
        //Task<List<Inventory>> GetInventories();
        Task ClearInventoryPages();
        Task Import(List<Inventory> inventories);
        Task ImportHtml(string invHtml, int pid);
        //string GetHtmlForId(int id);
        Task<string> GetHtmlForIdAsync(int productId);
    }
}
