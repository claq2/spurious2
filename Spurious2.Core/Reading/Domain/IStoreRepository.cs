using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spurious2.Core.Reading.Domain
{
    public interface IStoreRepository
    {
        IEnumerable<Store> GetStoresForSubdivision(int subdivisionId);
    }
}
