using Spurious2.Core2.Products;
using Spurious2.Core2.Stores;
using Spurious2.Core2.Subdivisions;

namespace Spurious2.Core2;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "<Pending>")]
public interface ISpuriousRepository : IDisposable
{
    Task CalculateBoundaryGeogs();
    Task ImportBoundaryBulk(List<BoundaryIncoming> boundaries);
    Task ClearPopulationIncoming();
    Task ImportPopulation(PopulationIncoming population);
    Task UpdatePopulationsFromIncoming();
    Task UpdateBoundariesFromIncoming();
    Task ClearBoundaryIncoming();
    Task ImportBoundary(BoundaryIncoming boundary);
    Task ImportStoresFromCsv(IAsyncEnumerable<StoreIncoming> stores);
    Task ImportPopulations(IAsyncEnumerable<PopulationIncoming> populations);
    Task ImportBoundaries(IAsyncEnumerable<BoundaryIncoming> boundaries);
    Task<List<Subdivision>> GetSubdivisionsForDensity(AlcoholType alcoholType, EndOfDistribution endOfDistribution, int limit, CancellationToken cancellationToken);
    Task<string> GetBoundaryForSubdivision(int subdivisionId, CancellationToken cancellationToken);
    Task<List<Store>> GetStoresBySubdivisionId(int subdivisionId, CancellationToken cancellationToken);
    Task ClearIncomingStores();
    Task ClearIncomingProducts();
    Task ClearIncomingInventory();
    Task UpdateIncomingStore(StoreIncoming store);
    Task ImportAFewProducts(List<ProductIncoming> products);
}
