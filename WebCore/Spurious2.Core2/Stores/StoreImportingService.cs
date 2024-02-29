using System.Globalization;
using CsvHelper;

namespace Spurious2.Core2.Stores;

public class StoreImportingService(ISpuriousRepository spuriousRepository) : IStoreImportingService
{
    private bool _disposedValue;

    public IEnumerable<StoreIncoming> ReadStores()
    {
        using var reader = new StreamReader("stores.csv");
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var stores = new List<StoreIncoming>();

        _ = csv.Read();
        _ = csv.ReadHeader();
        while (csv.Read())
        {
            if (csv.TryGetField<int>("Id", out var storeId)
                && csv.TryGetField<string>("WKT", out var wkt)
                && csv.TryGetField<string>("StoreName", out var storeName)
                && csv.TryGetField<string>("City", out var storeCity)
                && csv.TryGetField<int>("BeerVolume", out var beerVolume)
                && csv.TryGetField<int>("WineVolume", out var wineVolume)
                && csv.TryGetField<int>("SpiritsVolume", out var spiritsVolume)
                )
            {
                stores.Add(new StoreIncoming
                {
                    Id = storeId,
                    StoreName = storeName,
                    City = storeCity,
                    LocationWellKnownText = wkt,
                    BeerVolume = beerVolume,
                    WineVolume = wineVolume,
                    SpiritsVolume = spiritsVolume,
                    //LocationGeog = new NetTopologySuite.Geometries.Point(pointX, pointY)
                });
            }
        }

        spuriousRepository.ImportStores(stores);
        return stores;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposedValue)
        {
            if (disposing)
            {
                spuriousRepository?.Dispose();
            }

            this._disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
