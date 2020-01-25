using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spurious2.Core.SubdivisionImporting.Domain
{
    public interface ISubdivisionRepository : IDisposable
    {
        Task UpdateSubdivisionVolumes();
        void Import(IEnumerable<SubdivisionPopulation> populations);
        void Import(IEnumerable<SubdivisionBoundary> boundaries);
    }
}
