using Ardalis.Specification.EntityFrameworkCore;

namespace Spurious2.Infrastructure;

public class SpuriousSpecRepository<T>(SpuriousContext dbContext) : RepositoryBase<T>(dbContext) where T : class
{

}
