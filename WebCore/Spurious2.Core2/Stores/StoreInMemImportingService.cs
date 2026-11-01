using CsvHelper;
using NetTopologySuite.Geometries;
using Spurious2.Core2.Inventories;
using Spurious2.Core2.Properties;
using Spurious2.Core2.Subdivisions;

namespace Spurious2.Core2.Stores;

public class StoreInMemImportingService : IStoreInMemImportingService
{
    public List<Store> ImportWithData()
    {
        using var reader = new StringReader(Resources.cachedstores);
        using var csv = CsvReader.Create(reader);
        //csv.Context.RegisterClassMap<StoreMap>();

        var stores = csv.GetRecords<Store>().ToList();
        return stores;
    }



    //public sealed class StoreMap : ClassMap<Store>
    //{
    //    public StoreMap()
    //    {
    //        this.Map(m => m.Id);
    //        this.Map(m => m.StoreName);
    //        this.Map(m => m.City);
    //        this.Map(m => m.SpiritsVolume);
    //        this.Map(m => m.WineVolume);
    //        this.Map(m => m.BeerVolume);
    //        this.Map(m => m.SubdivisionId);
    //        this.Map(m => m.LocationGeog).Name("LocationWkt").TypeConverter<GeographyConverter>();
    //    }
    //}
}

[CsvRecordMap(typeof(Store))]
public sealed partial class StoreMap34
{
    [CsvColumn]
    public int Id { get; set; }

    [CsvColumn]
    public string? StoreName { get; set; } = string.Empty;

    [CsvColumn]
    public string? City { get; set; } = string.Empty;

    [CsvColumn]
    public int? BeerVolume { get; set; }

    [CsvColumn]
    public int? WineVolume { get; set; }

    [CsvColumn]
    public int? SpiritsVolume { get; set; }

    [CsvColumn("LocationWkt")]
    [CsvConverter(typeof(GeographyConverter))]
    public Point LocationGeog { get; set; } = Point.Empty;

    [CsvIgnore]
    public GeoJSON.Text.Geometry.Point Location { get; set; } = new GeoJSON.Text.Geometry.Point();

    [CsvIgnore]
    public ICollection<Inventory> Inventories { get; set; }

    [CsvIgnore]
    public int? SubdivisionId { get; set; }
}
