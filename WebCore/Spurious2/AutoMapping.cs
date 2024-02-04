using AutoMapper;
using Spurious2.Stores;

namespace Spurious2;

public class AutoMapping : Profile
{
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
        /*
         * "id": 39,
    "locationCoordinates": null,
    "name": null,
    "city": "COBOURG",
    "inventories": null
         */
    }
}
