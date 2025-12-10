using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Spurious2.Core2.Properties;
using Spurious2.Core2.Subdivisions;

namespace Spurious2.Core2.Stores;

public class StoreInMemImportingService : IStoreInMemImportingService
{
    public List<Store> ImportWithData()
    {
        using var reader = new StringReader(Resources.cachedstores);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<StoreMap>();

        var stores = csv.GetRecords<Store>().ToList();
        return stores;
    }

    public sealed class StoreMap : ClassMap<Store>
    {
        public StoreMap()
        {
            Map(m => m.Id);
            Map(m => m.StoreName);
            Map(m => m.City);
            Map(m => m.SpiritsVolume);
            Map(m => m.WineVolume);
            Map(m => m.BeerVolume);
            Map(m => m.SubdivisionId);
            Map(m => m.LocationGeog).Name("LocationWkt").TypeConverter<GeographyConverter>();
        }
    }
}
