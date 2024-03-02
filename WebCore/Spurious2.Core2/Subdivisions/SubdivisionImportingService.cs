using System.Globalization;
using System.Text;
using CsvHelper;
using Spurious2.Core2;
using Spurious2.Core2.Subdivisions;

namespace Spurious2.Core.SubdivisionImporting.Services;

public class SubdivisionImportingService(ISpuriousRepository spuriousRepository) : ISubdivisionImportingService
{
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            spuriousRepository?.Dispose();
        }
    }

    public IEnumerable<BoundaryIncoming> ImportBoundaryFromCsvFile(string filenameAndPath)
    {
        using var reader = new StreamReader(filenameAndPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var boundaries = new List<BoundaryIncoming>();

        _ = csv.Read();
        _ = csv.ReadHeader();
        while (csv.Read())
        {
            if (csv.TryGetField<int>("CSDUID", out var csduid)
                && csv.TryGetField<string>("WKT", out var wkt)
                && csv.TryGetField<string>("CSDNAME", out var csdname)
                && csv.TryGetField<int>("PRUID", out var prid)
                )
            {
                boundaries.Add(new BoundaryIncoming
                {
                    Id = csduid,
                    BoundaryWellKnownText = wkt,
                    SubdivisionName = csdname,
                    Province = string.Empty,
                });
            }

            spuriousRepository.ImportBoundaries(boundaries);
        }

        return boundaries;
    }

    public IEnumerable<PopulationIncoming> ImportPopulationFrom98File(string filenameAndPath)
    {
        // First pass is to extract province/territory names and IDs
        var provincesDict = new Dictionary<int, string>();
        using var provinceReader = new StreamReader(filenameAndPath, Encoding.UTF8);
        using var provinceCsv = new CsvReader(provinceReader, CultureInfo.InvariantCulture);
        {

            _ = provinceCsv.Read();
            _ = provinceCsv.ReadHeader();
            while (provinceCsv.Read())
            {
                if (provinceCsv.TryGetField<int>("ALT_GEO_CODE", out var id)
                    && provinceCsv.TryGetField<string>("GEO_NAME", out var name)
                    && provinceCsv.TryGetField<string>("GEO_LEVEL", out var level)
                )
                {
                    if (string.Equals(level, "Province", StringComparison.Ordinal)
                        || string.Equals(level, "Territory", StringComparison.Ordinal)
                    // && string.Compare(charName, "Population, 2021", StringComparison.Ordinal) == 0
                    )
                    {
                        provincesDict[id] = name;
                    }
                }
            }
        }

        // Second pass is to read populations
        using var reader = new StreamReader(filenameAndPath, Encoding.UTF8);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var records = new List<PopulationIncoming>();

        _ = csv.Read();
        _ = csv.ReadHeader();
        while (csv.Read())
        {
            if (csv.TryGetField<int>("ALT_GEO_CODE", out var id)
                && csv.TryGetField<string>("GEO_NAME", out var name)
                && csv.TryGetField<string>("GEO_LEVEL", out var level)
                && csv.TryGetField<int>("CHARACTERISTIC_ID", out var charId)
                && csv.TryGetField<string>("CHARACTERISTIC_NAME", out var charName)
            )
            {
                if (string.Equals(level, "Census subdivision", StringComparison.Ordinal)
                    // && string.Compare(charName, "Population, 2021", StringComparison.Ordinal) == 0
                    && charId == 1 // Population, 2021
                    )
                {
                    var provinceId = Convert.ToInt32(id.ToString(CultureInfo.InvariantCulture)[..2], CultureInfo.InvariantCulture);
                    var populationString = csv.GetField("C1_COUNT_TOTAL");
                    var population = !string.IsNullOrEmpty(populationString) ? Convert.ToInt32(populationString, CultureInfo.InvariantCulture) : 0;
                    //if (provincesDict[provinceId] == "Ontario")
                    //{
                    records.Add(new PopulationIncoming
                    {
                        Id = id,
                        // TODO: Don't send this subdiv name
                        // SubdivisionName = name, // Name values in 98 file are bad because they have "Town", "City" e.g. Mount Carmel-Mitchells Brook-St. Catherine's, Town (T)
                        Population = population,
                        Province = provincesDict[provinceId]
                    });
                    //}
                }
            }
        }

        spuriousRepository.ImportPopulations(records);
        return records;
    }
}
