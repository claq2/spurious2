using System;
using System.Collections.Generic;
using System.Linq;

namespace Spurious2.Core.SubdivisionImporting.Domain
{
    public interface IPopulationRespository : IDisposable
    {
        void Import(IEnumerable<SubdivisionPopulation> populations);
    }
}
