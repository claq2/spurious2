using MediatR;

namespace Spurious2.Core2.Densities;

public class GetDensitiesHandler : IRequestHandler<GetDensitiesRequest, List<DensityInfo>>
{
    internal const string top10Density = "top10";
    internal const string top10BeerDensity = "top10Beer";
    internal const string top10WineDensity = "top10Wine";
    internal const string top10SpiritsDensity = "top10Spirits";
    internal const string bottom10Density = "bottom10";
    internal const string allDensity = "all";

    private static readonly List<string> densities =
    [
        top10Density,
        top10BeerDensity,
        top10WineDensity,
        top10SpiritsDensity,
        bottom10Density,
        allDensity
    ];

    private static readonly Dictionary<string, string> densityToNameMap = new()
    {
        { top10Density, "Top 10 Overall" },
        { top10BeerDensity, "Top 10 Beer" },
        { top10WineDensity, "Top 10 Wine" },
        { top10SpiritsDensity, "Top 10 Spirits" },
        { bottom10Density, "Bottom 10 Overall" },
        { allDensity, "All" },
    };

    public Task<List<DensityInfo>> Handle(GetDensitiesRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var response = new List<DensityInfo>();
        foreach (var density in densities)
        {
            response.Add(new DensityInfo
            {
                ShortName = density,
                Title = densityToNameMap[density],
                Address = new Uri($"/densities/{density}/subdivisions", UriKind.Relative),
            });
        }

        return Task.FromResult(response);
    }
}
