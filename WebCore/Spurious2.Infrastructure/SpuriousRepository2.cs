using Spurious2.Core;
using Spurious2.Core2.Stores;

namespace Spurious2.Infrastructure
{
    public class SpuriousRepository2(Models.SpuriousContext dbContext) : ISpuriousRepository
    {
        public List<Store> GetStoresBySubdivisionId(int subdivisionId)
        {
            var subdiv = dbContext.Subdivisions.Single(s => s.Id == subdivisionId);
            var stores = dbContext.Stores.Where(s => s.Location.Intersects(subdiv.Boundary)).ToList();
            return stores;
        }
    }
}
