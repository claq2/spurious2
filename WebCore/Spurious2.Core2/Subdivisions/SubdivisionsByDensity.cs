using System.Linq.Expressions;
using Ardalis.Specification;
using Spurious2.Core2.Stores;

namespace Spurious2.Core2.Subdivisions;

public class SubdivisionsByDensity : Specification<Subdivision>
{
    internal static readonly Dictionary<AlcoholType, Expression<Func<Subdivision, object?>>> map = new()
    {
        { AlcoholType.All, s => s.AlcoholDensity },
        { AlcoholType.Beer, s => s.BeerDensity },
        { AlcoholType.Spirits, s => s.SpiritsDensity },
        { AlcoholType.Wine, s => s.WineDensity },
    };

    public SubdivisionsByDensity(AlcoholType alcoholType,
        EndOfDistribution endOfDistribution,
        int limit)
    {
        var keySelector = map[alcoholType];
        var subdivsQuery = this.Query
            .Where(s => s.AlcoholDensity > 0);
        subdivsQuery = //subdivsQuery.OrderBy(s => s.AlcoholDensity).Take(limit);
            DetermineOrderQuery(subdivsQuery, keySelector, endOfDistribution)
            .Take(limit);

        static ISpecificationBuilder<Subdivision> DetermineOrderQuery(ISpecificationBuilder<Subdivision> subdivsQuery,
            Expression<Func<Subdivision, object?>> keySelector,
            EndOfDistribution endOfDistribution)
           => endOfDistribution == EndOfDistribution.Top ?
               subdivsQuery.OrderByDescending(keySelector)
               : subdivsQuery.OrderBy(keySelector);
    }
}
