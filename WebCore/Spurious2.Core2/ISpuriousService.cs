using Spurious2.Core2.Stores;
using Spurious2.Core2.Subdivisions;

namespace Spurious2.Core2;

public interface ISpuriousService
{
    public Task<string> GetBoundaryForSubdivision(int subdivisionId, CancellationToken cancellationToken);
    public Task<List<Store>> GetStoresBySubdivisionId(int subdivisionId, CancellationToken cancellationToken);
    public Task<List<Subdivision>> GetSubdivisionsForDensity(AlcoholType alcoholType, EndOfDistribution endOfDistribution, int limit, CancellationToken cancellationToken);
}
