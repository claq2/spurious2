using Ardalis.Specification;

namespace Spurious2.Core2.Stores;

public class StoresBySubdivision : Specification<Store>
{
    public StoresBySubdivision(int subdivisionId)
    {
        this.Query.Where(s => s.SubdivisionId == subdivisionId);
    }
}
