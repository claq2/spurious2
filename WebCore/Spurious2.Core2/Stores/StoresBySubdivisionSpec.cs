using Ardalis.Specification;

namespace Spurious2.Core2.Stores;

public class StoresBySubdivisionSpec : Specification<Store>
{
    public StoresBySubdivisionSpec(int subdivisionId)
    {
        this.Query.Where(s => s.SubdivisionId == subdivisionId);
    }
}
