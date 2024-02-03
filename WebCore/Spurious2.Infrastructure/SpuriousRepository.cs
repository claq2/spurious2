using Ardalis.Specification.EntityFrameworkCore;
using Spurious2.Core;
using Spurious2.Core2.Stores;

namespace Spurious2.Infrastructure;

public class SpuriousRepository<T>(Models.SpuriousContext dbContext) : RepositoryBase<T>(dbContext)
where T : class
{

}

public class SpuriousRepository2(Models.SpuriousContext dbContext) : ISpuriousRepository
{
    public List<Store> X()
    {
        var subdiv = dbContext.Subdivisions.Single(s => s.Id == 3514021);
        var stores = dbContext.Stores.Where(s => s.Location.Intersects(subdiv.Boundary)).ToList();
        return stores;
    }
}
