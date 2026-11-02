

using Spurious2.Core2.Stores;
using Spurious2.Core2.Subdivisions;

using var storeReader = CsvHelper.CsvReader.FromFile("stores.csv");
await foreach (var record in storeReader.GetRecordsAsync<StoreIncoming>().ConfigureAwait(false))
{
    Console.WriteLine($"Id: {record.Id}, StoreName: {record.StoreName}, City: {record.City}, LocationWkt: {record.LocationGeog?.X}, {record.LocationGeog?.Y} , BeerVolume: {record.BeerVolume}, WineVolume: {record.WineVolume}, SpiritsVolume: {record.SpiritsVolume}");
}

using var populationReader = CsvHelper.CsvReader.FromFile("population.csv");
var provinceDict = new Dictionary<int, string>();
await foreach (var record in populationReader.GetRecordsAsync<PopulationIncoming>().ConfigureAwait(false))
{
    if (record.GeoLevel == "Province")
    {
        provinceDict[record.Id] = record.GeoName ?? string.Empty;
    }

    if (record.CharacteristicName != "Population, 2021" || record.CharacteristicId != 1)
    {
        Console.WriteLine($"Id: {record.Id}, GeoLevel: {record.GeoLevel}, Geo Name: {record.GeoName}, Characteristic Name: {record.CharacteristicName}, Population: {record.Population}");
    }
}

foreach (var keyPair in provinceDict)
{
    Console.WriteLine($"{keyPair.Key} = {keyPair.Value}");
}

using var subdivisionReader = CsvHelper.CsvReader.FromFile("subdiv.csv");
