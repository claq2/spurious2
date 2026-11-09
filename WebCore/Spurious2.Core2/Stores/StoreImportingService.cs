using CsvHelper;

namespace Spurious2.Core2.Stores;

public class StoreImportingService(ISpuriousRepository spuriousRepository) : IStoreImportingService
{
    public async Task ImportStoresFromCsvFile(string filenameAndPath)
    {
        //static async IAsyncEnumerable<StoreIncoming> ReadStores(string filenameAndPath)
        //{
        //    using var reader = new StreamReader(filenameAndPath);
        //    using var csv = CsvReader.Create(reader);

        //    //_ = await csv.ReadAsync().ConfigAwait();
        //    //_ = csv.ReadHeader();
        //    var idColumnIndex = csv.GetFieldIndex("Id");
        //    var wktColumnIndex = csv.GetFieldIndex("WKT");
        //    var storeNameColumnIndex = csv.GetFieldIndex("StoreName");
        //    var cityColumnIndex = csv.GetFieldIndex("City");
        //    var beerVolumeColumnIndex = csv.GetFieldIndex("BeerVolume");
        //    var wineVolumeColumnIndex = csv.GetFieldIndex("WineVolume");
        //    var spiritsVolumeColumnIndex = csv.GetFieldIndex("SpiritsVolume");
        //    while (await csv.ReadAsync().ConfigAwait())
        //    {
        //        if (csv.TryParse<int>(idColumnIndex, out var storeId)
        //            && csv.TryParse<string>(wktColumnIndex, out var wkt)
        //            && csv.TryParse<string>(storeNameColumnIndex, out var storeName)
        //            && csv.TryParse<string>(cityColumnIndex, out var storeCity)
        //            && csv.TryParse<int>(beerVolumeColumnIndex, out var beerVolume)
        //            && csv.TryParse<int>(wineVolumeColumnIndex, out var wineVolume)
        //            && csv.TryParse<int>(spiritsVolumeColumnIndex, out var spiritsVolume)
        //            )
        //        {
        //            yield return new StoreIncoming
        //            {
        //                Id = storeId,
        //                StoreName = storeName,
        //                City = storeCity,
        //                LocationWellKnownText = wkt,
        //                BeerVolume = beerVolume,
        //                WineVolume = wineVolume,
        //                SpiritsVolume = spiritsVolume,
        //            };
        //        }
        //    }
        //}

        using var storeReader = CsvReader.FromFile(filenameAndPath);
        var stores = storeReader.GetRecordsAsync<StoreIncoming>();

        await spuriousRepository.ImportStoresFromCsv(stores).ConfigAwait();
    }
}
