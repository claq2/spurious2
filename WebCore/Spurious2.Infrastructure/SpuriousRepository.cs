﻿using Ardalis.Specification.EntityFrameworkCore;

namespace Spurious2.Infrastructure;

public class SpuriousRepository<T>(Models.SpuriousContext dbContext) : RepositoryBase<T>(dbContext)
where T : class
{

}