using Spurious2.Core.Populations;
using Spurious2.Core2.Stores;
using Spurious2.Core2.Subdivisions;

namespace Spurious2.Core2;

public interface ISpuriousRepository : IDisposable
{
    void ImportStores(IEnumerable<StoreIncoming> stores);
    void ImportPopulations(IEnumerable<PopulationIncoming> populations);
    void ImportBoundaries(IEnumerable<BoundaryIncoming> boundaries);
    Task<List<Subdivision>> GetSubdivisionsForDensity(AlcoholType alcoholType, EndOfDistribution endOfDistribution, int limit, CancellationToken cancellationToken);
    Task<string> GetBoundaryForSubdivision(int subdivisionId, CancellationToken cancellationToken);
    Task<List<Store>> GetStoresBySubdivisionId(int subdivisionId, CancellationToken cancellationToken);
}
