using System.Collections.ObjectModel;
using AutoMapper;
using Spurious2.Stores;

namespace Spurious2;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        _ = this.CreateMap<Core2.Stores.Store, Store>()
            .ForMember(d => d.Name, c => c.MapFrom(s => s.StoreName))
            .ForMember(d => d.LocationCoordinates, c => c.MapFrom(s => s.Location))
            .ForMember(d => d.Inventories, c => c.MapFrom(s => new ReadOnlyCollection<Inventory>(new List<Inventory>
            {
                new () { AlcoholType = Core2.Stores.AlcoholType.Beer, Volume = (s.BeerVolume ?? 0) / 1000 },
                new () { AlcoholType = Core2.Stores.AlcoholType.Spirits, Volume = (s.SpiritsVolume ?? 0) / 1000 },
                new () { AlcoholType = Core2.Stores.AlcoholType.Wine, Volume = (s.WineVolume ?? 0) / 1000 },
            })));

        _ = this.CreateMap<Core2.Subdivisions.Subdivision, Subdivisions.Subdivision>()
            .ForMember(d => d.Name, c => c.MapFrom(s => s.SubdivisionName))
            .ForMember(d => d.CentreCoordinates, c => c.MapFrom(s => s.GeographicCentre))
            .ForMember(d => d.BoundaryLink, c => c.MapFrom(s => new Uri($"/subdivisions/{s.Id}/boundary", UriKind.Relative)));
    }
}
