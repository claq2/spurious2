using System;
using System.Collections.Generic;
using System.Linq;

namespace Spurious2.Core.Reading.Domain
{
    public interface ISubdivisionRepository
    {
        //List<Subdivision> GetSubdivisionsForDensity(string alcoholType, string endOfDistribution, int limit);
        List<Subdivision> GetSubdivisionsForDensity(AlcoholType alcoholType, EndOfDistribution endOfDistribution, int limit);
        string GetBoundaryForSubdivision(int id);
    }
}
