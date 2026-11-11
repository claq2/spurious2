using System.Globalization;
using Spurious2.Core2.Stores;
using Spurious2.Core2.Subdivisions;
using Spurious2.Infrastructure.Importing;

// Make sure Infrastructure assembly is loaded so that the CsvHelper maps are registered
var _ = typeof(StoreIncomingCsvMap).Assembly;

// Stores
using var storeReader = CsvHelper.CsvReader.FromFile("stores.csv");
await foreach (var record in storeReader.GetRecordsAsync<StoreIncoming>().ConfigureAwait(false))
{
    Console.WriteLine($"Id: {record.Id}, StoreName: {record.StoreName}, City: {record.City}, LocationWkt: {record.LocationWellKnownText}, BeerVolume: {record.BeerVolume}, WineVolume: {record.WineVolume}, SpiritsVolume: {record.SpiritsVolume}");
}

// Population
using var populationReader = CsvHelper.CsvReader.FromFile("population.csv");
var provinceDict = new Dictionary<int, string>();
await foreach (var record in populationReader.GetRecordsAsync<PopulationIncoming>().ConfigureAwait(false))
{
    if (record.GeoLevel is "Province" or "Territory")
    {
        provinceDict[record.Id] = record.GeoName ?? string.Empty;
    }

    if (record.GeoLevel is "Census subdivision" or "Census division" or "Province" or "Territory")
    {
        var first2Chars = record.Id.ToString(CultureInfo.InvariantCulture)[..2];
        record.Province = provinceDict.TryGetValue(int.Parse(first2Chars, CultureInfo.InvariantCulture), out var provinceName) ? provinceName : string.Empty;
    }

    Console.WriteLine($"Id: {record.Id}, Province: {record.Province}, GeoLevel: {record.GeoLevel}, Geo Name: {record.GeoName}, Characteristic Name: {record.CharacteristicName}, Population: {record.Population}");
}

foreach (var keyPair in provinceDict)
{
    Console.WriteLine($"{keyPair.Key} = {keyPair.Value}");
}

// Subdivisions
using var subdivisionReader = CsvHelper.CsvReader.FromFile("subdiv.csv");
await foreach (var record in subdivisionReader.GetRecordsAsync<BoundaryIncoming>().ConfigureAwait(false))
{
    Console.WriteLine($"Id: {record.Id}, SubdivisionName: {record.SubdivisionName}, BoundaryWkt: {record.BoundaryWellKnownText?[..20]}");
}
