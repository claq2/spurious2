using System.Globalization;

namespace Spurious2.Core2.Stores;

public class StoreImportingService(ISpuriousRepository spuriousRepository) : IStoreImportingService
{
    public IEnumerable<StoreIncoming> ReadStores()
    {
        // TODO: Use CsvReader
        var lines = File.ReadLines("stores.csv");
        var stores = new List<StoreIncoming>();
        foreach (var line in lines)
        {
            var elements = line.Split(',');
            // POINT (-79.531037 43.712679)
            //var pointValues = elements[6].Split(' ');
            //var pointX = Convert.ToDouble(pointValues[1][1..], CultureInfo.InvariantCulture);
            //var pointY = Convert.ToDouble(pointValues[2][..^1], CultureInfo.InvariantCulture);
            stores.Add(new StoreIncoming
            {
                Id = Convert.ToInt32(elements[0], CultureInfo.InvariantCulture),
                StoreName = elements[1],
                City = elements[2],
                LocationWellKnownText = elements[6],
                //BeerVolume = elements[3] != "NULL" ? Convert.ToInt32(elements[3], CultureInfo.InvariantCulture) : 0,
                //WineVolume = elements[4] != "NULL" ? Convert.ToInt32(elements[4], CultureInfo.InvariantCulture) : 0,
                //SpiritsVolume = elements[5] != "NULL" ? Convert.ToInt32(elements[5], CultureInfo.InvariantCulture) : 0,
                //LocationGeog = new NetTopologySuite.Geometries.Point(pointX, pointY)
            });
        }

        spuriousRepository.ImportStores(stores);
        return stores;
    }
}
