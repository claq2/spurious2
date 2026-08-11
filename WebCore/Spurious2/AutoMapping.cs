using System.Collections.ObjectModel;
using Riok.Mapperly.Abstractions;
using Spurious2.Stores;

namespace Spurious2;

[Mapper]
public static partial class AutoMappingX
{
    [MapProperty(nameof(Core2.Stores.Store.StoreName), nameof(Store.Name))]
    [MapProperty(nameof(Core2.Stores.Store.Location), nameof(Store.LocationCoordinates))]
    [MapPropertyFromSource(nameof(Store.Inventories), Use = nameof(CreateInventories))]
    public static partial Store ToStore(this Core2.Stores.Store source);

    [MapProperty(nameof(Core2.Subdivisions.Subdivision.SubdivisionName), nameof(Subdivisions.Subdivision.Name))]
    [MapProperty(nameof(Core2.Subdivisions.Subdivision.GeographicCentre), nameof(Subdivisions.Subdivision.CentreCoordinates))]
    [MapProperty(nameof(Core2.Subdivisions.Subdivision.Id), nameof(Subdivisions.Subdivision.BoundaryLink), Use = nameof(CreateBoundaryLink))]
    [MapperIgnoreSource(nameof(Core2.Subdivisions.Subdivision.BeerVolume))]
    [MapperIgnoreSource(nameof(Core2.Subdivisions.Subdivision.WineVolume))]
    [MapperIgnoreSource(nameof(Core2.Subdivisions.Subdivision.SpiritsVolume))]
    [MapperIgnoreSource(nameof(Core2.Subdivisions.Subdivision.BeerDensity))]
    [MapperIgnoreSource(nameof(Core2.Subdivisions.Subdivision.WineDensity))]
    [MapperIgnoreSource(nameof(Core2.Subdivisions.Subdivision.SpiritsDensity))]
    [MapperIgnoreSource(nameof(Core2.Subdivisions.Subdivision.MedianIncome))]
    [MapperIgnoreSource(nameof(Core2.Subdivisions.Subdivision.AverageIncome))]
    [MapperIgnoreSource(nameof(Core2.Subdivisions.Subdivision.Province))]
    [MapperIgnoreSource(nameof(Core2.Subdivisions.Subdivision.AverageAfterTaxIncome))]
    [MapperIgnoreSource(nameof(Core2.Subdivisions.Subdivision.MedianAfterTaxIncome))]
    [MapperIgnoreSource(nameof(Core2.Subdivisions.Subdivision.Boundary))]
    [MapperIgnoreSource(nameof(Core2.Subdivisions.Subdivision.GeographicCentreGeog))]
    [MapperIgnoreSource(nameof(Core2.Subdivisions.Subdivision.AlcoholDensity))]
    public static partial Subdivisions.Subdivision ToSubdivision(this Core2.Subdivisions.Subdivision source);


    private static ReadOnlyCollection<Inventory> CreateInventories(Core2.Stores.Store source) =>
        new(
        [
            new() { AlcoholType = Core2.Stores.AlcoholType.Beer, Volume = (source.BeerVolume ?? 0) / 1000m },
            new() { AlcoholType = Core2.Stores.AlcoholType.Spirits, Volume = (source.SpiritsVolume ?? 0) / 1000m },
            new() { AlcoholType = Core2.Stores.AlcoholType.Wine, Volume = (source.WineVolume ?? 0) / 1000m },
        ]);

    private static Uri CreateBoundaryLink(int id) => new($"/subdivisions/{id}/boundary", UriKind.Relative);
}
