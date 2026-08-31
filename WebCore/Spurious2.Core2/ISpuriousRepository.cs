using Spurious2.Core2.Inventories;
using Spurious2.Core2.Products;
using Spurious2.Core2.Stores;
using Spurious2.Core2.Subdivisions;

namespace Spurious2.Core2;

public interface ISpuriousRepository : IDisposable
{
    public Task<bool> AnyIncomingRecordsNotDone(CancellationToken cancellationToken);
    public Task<List<int>> AddIncomingStoreIdsAndReturnAddedIds(IEnumerable<int> storeIds);
#pragma warning disable CA1002 // Do not expose generic lists
    public Task<List<int>> GetStoresToBeAdded(List<int> storeIds);
#pragma warning restore CA1002 // Do not expose generic lists
    public Task CalculateBoundaryGeogs();
    public Task ImportBoundaryBulk(IEnumerable<BoundaryIncoming> boundaries);
    public Task ClearPopulationIncoming();
    public Task ImportPopulation(PopulationIncoming population);
    public Task UpdatePopulationsFromIncoming();
    public Task UpdateBoundariesFromIncoming();
    public Task ClearBoundaryIncoming();
    public Task ImportBoundary(BoundaryIncoming boundary);
    public Task ImportStoresFromCsv(IAsyncEnumerable<StoreIncoming> stores);
    public Task ImportPopulations(IAsyncEnumerable<PopulationIncoming> populations);
    public Task ImportBoundaries(IAsyncEnumerable<BoundaryIncoming> boundaries);
    public Task<List<Subdivision>> GetSubdivisionsForDensity(AlcoholType alcoholType, EndOfDistribution endOfDistribution, int limit, CancellationToken cancellationToken);
    public Task<string> GetBoundaryForSubdivision(int subdivisionId, CancellationToken cancellationToken);
    public Task<List<Store>> GetStoresBySubdivisionId(int subdivisionId, CancellationToken cancellationToken);
    public Task ClearIncomingStores();
    public Task ClearIncomingProducts();
    public Task ClearIncomingInventory();
    public Task UpdateIncomingStore(StoreIncoming store, CancellationToken cancellationToken);
    public Task<int> ImportAFewProducts(IEnumerable<ProductIncoming> products);
    public Task AddIncomingStoreIds(IEnumerable<int> storeIds);
    public Task AddIncomingInventories(IEnumerable<InventoryIncoming> inventories);
    public Task MarkIncomingProductDone(string productId);
    public Task UpdateStoresFromIncoming();
    public Task UpdateProductsFromIncoming();
    public Task UpdateInventoriesFromIncoming();
    public Task UpdateStoreVolumes();
    public Task UpdateSubdivisionVolumes();
    public Task UpdateAllFromIncoming();
}
