using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using Spurious2.Core;
using Spurious2.Core.Reading.Domain;
using Spurious2.Web.ServiceModel;

namespace Spurious2.Web.ServiceInterface;

public class MyServices(IStoreRepository storeRespoistory, ISubdivisionRepository subdivisionRepository) : Service
{
    private const string top10Density = "top10";
    private const string top10BeerDensity = "top10Beer";
    private const string top10WineDensity = "top10Wine";
    private const string top10SpiritsDensity = "top10Spirits";
    private const string bottom10Density = "bottom10";
    private const string allDensity = "all";

    private static readonly List<string> densities =
    [
        top10Density,
        top10BeerDensity,
        top10WineDensity,
        top10SpiritsDensity,
        bottom10Density,
        allDensity
    ];

    private static readonly Dictionary<string, (AlcoholType at, EndOfDistribution e, int l)> densityToParametersMap = new()
    {
        { top10Density, (AlcoholType.All, EndOfDistribution.Top, 10) },
        { top10BeerDensity, (AlcoholType.Beer, EndOfDistribution.Top, 10) },
        { top10WineDensity, (AlcoholType.Wine, EndOfDistribution.Top, 10) },
        { top10SpiritsDensity, (AlcoholType.Spirits, EndOfDistribution.Top, 10) },
        { bottom10Density, (AlcoholType.All, EndOfDistribution.Bottom, 10) },
        { allDensity, (AlcoholType.All, EndOfDistribution.Top, 10000) },
    };

    private static readonly Dictionary<string, string> densityToNameMap = new()
    {
        { top10Density, "Top 10 Overall" },
        { top10BeerDensity, "Top 10 Beer" },
        { top10WineDensity, "Top 10 Wine" },
        { top10SpiritsDensity, "Top 10 Spirits" },
        { bottom10Density, "Bottom 10 Overall" },
        { allDensity, "All" },
    };

    //[Route("/densities")]
#pragma warning disable CA1822 // Mark members as static
    public List<DensityInfo> Get(Densities x)
#pragma warning restore CA1822 // Mark members as static
    {
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

        return response;
    }

    //[Route("/densities/{Name}/subdivisions")]
    public async Task<List<ServiceModel.Subdivision>> Get(DensitySubdivisions request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var result = new List<ServiceModel.Subdivision>();
        var (at, e, l) = densityToParametersMap[request.Name];
        var subdivs = await subdivisionRepository.GetSubdivisionsForDensity(at, e, l).ConfigAwait();
        result.AddRange(subdivs.Select(sd =>
        {
            var geocentre = JsonConvert.DeserializeObject<Point>(sd.Centre);
            var density = sd.Density / 1000;
            return new ServiceModel.Subdivision
            {
                BoundaryLink = new Uri($"/subdivisions/{sd.Id}/boundary", UriKind.Relative),
                CentreCoordinates = $"{geocentre.Coordinates.Longitude},{geocentre.Coordinates.Latitude}",
                Density = density,
                Name = sd.Name,
                Population = sd.Population,
                StoresLink = new Uri($"/subdivisions/{sd.Id}/stores", UriKind.Relative),
            };
        }));

        return result;
    }

    //[Route("/subdivisions/{Id}/boundary")]
    public async Task<string> Get(BoundaryRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return await subdivisionRepository.GetBoundaryForSubdivision(request.Id).ConfigAwait();
    }

    //[Route("/subdivisions/{Id}/stores")]
    public async Task<List<ServiceModel.Store>> Get(StoresInSubdivisionRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var result = (await storeRespoistory.GetStoresForSubdivision(request.Id).ConfigAwait())
            .Select(dbStore =>
            {
                var geolocation = JsonConvert.DeserializeObject<Point>(dbStore.Location);
                return new ServiceModel.Store
                {
                    Id = dbStore.Id,
                    Inventories =
                    [
                        new ServiceModel.Inventory { AlcoholType = AlcoholType.Beer, Volume = dbStore.BeerVolume / 1000 },
                        new ServiceModel.Inventory { AlcoholType = AlcoholType.Wine, Volume = dbStore.WineVolume / 1000 },
                        new ServiceModel.Inventory { AlcoholType = AlcoholType.Spirits, Volume = dbStore.SpiritsVolume / 1000 },
                    ],
                    LocationCoordinates = $"{geolocation.Coordinates.Longitude},{geolocation.Coordinates.Latitude}",
                    Name = dbStore.Name,
                    City = dbStore.City,
                };
            });

        return result.ToList();
    }
}
