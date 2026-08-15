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
            this.Map(m => m.Id);
            this.Map(m => m.StoreName);
            this.Map(m => m.City);
            this.Map(m => m.SpiritsVolume);
            this.Map(m => m.WineVolume);
            this.Map(m => m.BeerVolume);
            this.Map(m => m.SubdivisionId);
            this.Map(m => m.LocationGeog).Name("LocationWkt").TypeConverter<GeographyConverter>();
        }
    }
}
