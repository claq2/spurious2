using MediatR;
using Spurious2.Core2.Densities;
using Spurious2.Core2.Stores;

namespace Spurious2.Core2.Subdivisions;

public class GetSubdivisionsByDensityHandler(ISpuriousRepository spuriousRepository) : IRequestHandler<GetSubdivisionsByDensityRequest, List<Subdivision>>
{
    private static readonly Dictionary<string, (AlcoholType at, EndOfDistribution eod, int lim)> densityToParametersMap = new()
    {
        { GetDensitiesHandler.top10Density.ToUpperInvariant(), (AlcoholType.All, EndOfDistribution.Top, 10) },
        { GetDensitiesHandler.top10BeerDensity.ToUpperInvariant(), (AlcoholType.Beer, EndOfDistribution.Top, 10) },
        { GetDensitiesHandler.top10WineDensity.ToUpperInvariant(), (AlcoholType.Wine, EndOfDistribution.Top, 10) },
        { GetDensitiesHandler.top10SpiritsDensity.ToUpperInvariant(), (AlcoholType.Spirits, EndOfDistribution.Top, 10) },
        { GetDensitiesHandler.bottom10Density.ToUpperInvariant(), (AlcoholType.All, EndOfDistribution.Bottom, 10) },
        { GetDensitiesHandler.allDensity.ToUpperInvariant(), (AlcoholType.All, EndOfDistribution.Top, 10000) },
    };

    public async Task<List<Subdivision>> Handle(GetSubdivisionsByDensityRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var (at, eofd, lim) = densityToParametersMap[request.DensityName.ToUpperInvariant()];
        var subdivs = await spuriousRepository
            .GetSubdivisionsForDensity(at, eofd, lim, cancellationToken)
            .ConfigAwait();
        return subdivs;
    }
}
