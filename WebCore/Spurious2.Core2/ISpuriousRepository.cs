using Spurious2.Core2.Stores;

namespace Spurious2.Core;

public interface ISpuriousRepository
{
    Task<string> GetBoundaryForSubdivision(int subdivisionId);
    Task<List<Store>> GetStoresBySubdivisionId(int subdivisionId);
}
