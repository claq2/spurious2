﻿using Spurious2.Core2.Stores;
using Spurious2.Core2.Subdivisions;

namespace Spurious2.Core;

public interface ISpuriousRepository
{
    Task<List<Subdivision>> GetSubdivisionsForDensity(AlcoholType alcoholType, EndOfDistribution endOfDistribution, int limit);
    Task<string> GetBoundaryForSubdivision(int subdivisionId);
    Task<List<Store>> GetStoresBySubdivisionId(int subdivisionId);
}