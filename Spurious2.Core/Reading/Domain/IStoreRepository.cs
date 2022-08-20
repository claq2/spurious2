namespace Spurious2.Core.Reading.Domain;

public interface IStoreRepository
{
    Task<IEnumerable<Store>> GetStoresForSubdivision(int subdivisionId);
}
