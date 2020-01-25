using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spurious2.Core.LcboImporting.Domain
{
    public interface IStoreRepository : IDisposable
    {
        Task UpdateStoreVolumes();
        Task Import(IEnumerable<StoreInfo> storeInfos);
    }
}
