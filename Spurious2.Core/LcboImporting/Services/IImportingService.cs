using System;
using System.Threading.Tasks;

namespace Spurious2.Core.LcboImporting.Services
{
    public interface IImportingService : IDisposable
    {
        Task<int> UpdateInventoriesFromDatabase(int skip = 0, int take = 100000);
        Task<int> ReadInventoryHtmlsIntoDatabase();
        //Task<int> ImportInventories3();
        //Task<int> ImportInventories();
        Task<int> ImportProducts();
        Task<int> ImportStores();
        //Task<int> ImportInventories2();
    }
}
