using AutoMapper;
using Spurious2.Core2.Densities;
using Spurious2.Stores;

namespace Spurious2;

public class AutoMapping : Profile
{
    public const string RequestedDenstityItemId = "RequestedDenstity";

    public AutoMapping()
    {
        CreateMap<Core2.Stores.Store, Stores.Store>()
            .ForMember(d => d.Name, c => c.MapFrom(s => s.StoreName))
            .ForMember(d => d.LocationCoordinates, c => c.MapFrom(s => s.Location))
            .ForMember(d => d.Inventories, c => c.MapFrom(s => new List<Inventory>
            {
                new () { AlcoholType = Core2.Stores.AlcoholType.Beer, Volume = s.BeerVolume!.Value / 1000 },
                new () { AlcoholType = Core2.Stores.AlcoholType.Spirits, Volume = s.SpiritsVolume!.Value / 1000 },
                new () { AlcoholType = Core2.Stores.AlcoholType.Wine, Volume = s.WineVolume!.Value / 1000 },
            }));

        CreateMap<Core2.Subdivisions.Subdivision, Subdivisions.Subdivision>()
            .ForMember(d => d.RequestedDensityAmount,
                c => c.MapFrom((src, dest, destMember, context) =>
                    GetRequestedDensityAmount(src, context.Items[RequestedDenstityItemId] as string)))
            .ForMember(d => d.Name, c => c.MapFrom(s => s.SubdivisionName))
            .ForMember(d => d.CentreCoordinates, c => c.MapFrom(s => s.GeographicCentre))
            .ForMember(d => d.BoundaryLink, c => c.MapFrom(s => new Uri($"/subdivisions/{s.Id}/boundary", UriKind.Relative)))
            .ForMember(d => d.StoresLink, c => c.MapFrom(s => new Uri($"/subdivisions/{s.Id}/stores", UriKind.Relative)));

        /*
          "name": null,
    "population": 235,
    "density": 0,
    "boundaryLink": null,
    "centreCoordinates": null,
    "storesLink": null*/
    }

    private static decimal GetRequestedDensityAmount(Core2.Subdivisions.Subdivision subdivision, string densityName)
    {
        decimal result = 0;
        if (densityName == GetDensitiesHandler.top10Density
            || densityName == GetDensitiesHandler.bottom10Density
            || densityName == GetDensitiesHandler.allDensity)
        {
            result = subdivision.AlcoholDensity.Value;
        }
        else if (densityName == GetDensitiesHandler.top10BeerDensity)
        {
            result = subdivision.BeerDensity.Value;
        }
        else if (densityName == GetDensitiesHandler.top10WineDensity)
        {
            result = subdivision.WineDensity.Value;
        }
        else if (densityName == GetDensitiesHandler.top10SpiritsDensity)
        {
            result = subdivision.SpiritsDensity.Value;
        }

        return result;
    }
}
