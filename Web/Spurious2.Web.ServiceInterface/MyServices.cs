using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using Spurious2.Core.Reading.Domain;
using Spurious2.Web.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spurious2.Web.ServiceInterface
{
    public class MyServices : Service
    {
        const string top10Density = "top10";
        const string top10BeerDensity = "top10Beer";
        const string top10WineDensity = "top10Wine";
        const string top10SpiritsDensity = "top10Spirits";
        const string bottom10Density = "bottom10";
        const string allDensity = "all";

        private static readonly List<string> densities = new List<string>
        {
            top10Density, top10BeerDensity, top10WineDensity, top10SpiritsDensity, bottom10Density, allDensity
        };

        private static readonly Dictionary<string, (AlcoholType at, EndOfDistribution e, int l)> densityToParametersMap = new Dictionary<string, (AlcoholType at, EndOfDistribution e, int l)>
        {
            { top10Density, (AlcoholType.All, EndOfDistribution.Top, 10) },
            { top10BeerDensity, (AlcoholType.Beer, EndOfDistribution.Top, 10) },
            { top10WineDensity, (AlcoholType.Wine, EndOfDistribution.Top, 10) },
            { top10SpiritsDensity, (AlcoholType.Spirits, EndOfDistribution.Top, 10) },
            { bottom10Density, (AlcoholType.All, EndOfDistribution.Bottom, 10) },
            { allDensity, (AlcoholType.All, EndOfDistribution.Top, 10000) },
        };

        private static readonly Dictionary<string, string> densityToNameMap = new Dictionary<string, string>
        {
            { top10Density, "Top 10 Overall" },
            { top10BeerDensity, "Top 10 Beer" },
            { top10WineDensity, "Top 10 Wine" },
            { top10SpiritsDensity, "Top 10 Spirits" },
            { bottom10Density, "Bottom 10 Overall" },
            { allDensity, "All" },
        };

        private readonly Dictionary<EndOfDistribution, string> endOfDistributionMap = new Dictionary<EndOfDistribution, string>
        {
            { EndOfDistribution.Bottom, "asc" },
            { EndOfDistribution.Top, "desc" }
        };

        private readonly Dictionary<AlcoholType, string> alcoholTypeMap = new Dictionary<AlcoholType, string>
        {
            { AlcoholType.All, "all" },
            { AlcoholType.Beer, "beer_volume" },
            { AlcoholType.Wine, "wine_volume" },
            { AlcoholType.Spirits, "spirits_volume" },
        };

        readonly IStoreRepository storeRespoistory;
        readonly ISubdivisionRepository subdivisionRepository;

        public MyServices(IStoreRepository storeRespoistory, ISubdivisionRepository subdivisionRepository)
        {
            this.subdivisionRepository = subdivisionRepository;
            this.storeRespoistory = storeRespoistory;
        }

        //Return index.html for unmatched requests so routing is handled on client
        public object Any(FallbackForClientRoutes x) => Request.GetPageResult("/");

        //[Route("/densities")]
        public List<DensityInfo> Get(Densities x)
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
        public List<ServiceModel.Subdivision> Get(DensitySubdivisions request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new List<ServiceModel.Subdivision>();
            var (at, e, l) = densityToParametersMap[request.Name];
            var subdivs = this.subdivisionRepository.GetSubdivisionsForDensity(at, e, l);
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
        public string Get(BoundaryRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return this.subdivisionRepository.GetBoundaryForSubdivision(request.Id);
        }

        //[Route("/subdivisions/{Id}/stores")]
        public List<ServiceModel.Store> Get(StoresInSubdivisionRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = this.storeRespoistory.GetStoresForSubdivision(request.Id)
                .Select(dbStore =>
                {
                    var geolocation = JsonConvert.DeserializeObject<Point>(dbStore.Location);
                    return new ServiceModel.Store
                    {
                        Id = dbStore.Id,
                        Inventories = new List<ServiceModel.Inventory>
                        {
                            new ServiceModel.Inventory { AlcoholType = AlcoholType.Beer, Volume = dbStore.BeerVolume / 1000 },
                            new ServiceModel.Inventory { AlcoholType = AlcoholType.Wine, Volume = dbStore.WineVolume / 1000 },
                            new ServiceModel.Inventory { AlcoholType = AlcoholType.Spirits, Volume = dbStore.SpiritsVolume / 1000 },
                        },
                        LocationCoordinates = $"{geolocation.Coordinates.Longitude},{geolocation.Coordinates.Latitude}",
                        Name = dbStore.Name,
                    };
                });

            return result.ToList();
        }
    }
}
