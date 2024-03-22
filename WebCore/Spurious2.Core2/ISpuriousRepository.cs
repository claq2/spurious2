using Spurious2.Core2.Inventories;
using Spurious2.Core2.Products;
using Spurious2.Core2.Stores;
using Spurious2.Core2.Subdivisions;

namespace Spurious2.Core2;

public interface ISpuriousRepository : IDisposable
{
    Task CalculateBoundaryGeogs();
    Task ImportBoundaryBulk(IEnumerable<BoundaryIncoming> boundaries);
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
    Task<int> ImportAFewProducts(IEnumerable<ProductIncoming> products);
    Task AddIncomingStoreIds(IEnumerable<int> storeIds);
    Task AddIncomingInventories(IEnumerable<InventoryIncoming> inventories);
    Task MarkIncomingProductDone(string productId);
    Task UpdateStoresFromIncoming();
    Task UpdateProductsFromIncoming();
    Task UpdateInventoriesFromIncoming();
    Task UpdateStoreVolumes();
    Task UpdateSubdivisionVolumes();
}
