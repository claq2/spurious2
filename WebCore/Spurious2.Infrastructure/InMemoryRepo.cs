using Spurious2.Core2.Stores;
using Spurious2.Core2.Subdivisions;

namespace Spurious2.Infrastructure;

public class InMemoryRepo
{
    public InMemoryRepo(ISubdivisionInMemImportingService subdivisionImportingService
        , IStoreInMemImportingService storeImportingService
        )
    {
        ArgumentNullException.ThrowIfNull(subdivisionImportingService);
        ArgumentNullException.ThrowIfNull(storeImportingService);
        this.InMemSubdivisions.AddRange(subdivisionImportingService.ImportWithData());
        this.InMemStores.AddRange(storeImportingService.ImportWithData());
    }

#pragma warning disable CA1002 // Do not expose generic lists
    public List<Subdivision> InMemSubdivisions { get; } = [new()];
#pragma warning restore CA1002 // Do not expose generic lists

#pragma warning disable CA1002 // Do not expose generic lists
    public List<Store> InMemStores { get; } = [new()];
#pragma warning restore CA1002 // Do not expose generic lists
}
