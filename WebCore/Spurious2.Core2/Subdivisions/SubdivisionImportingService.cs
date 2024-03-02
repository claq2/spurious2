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

    public async Task ImportBoundaryFromCsvFile(string filenameAndPath)
    {
        //using var reader = new StreamReader(filenameAndPath);
        //using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        //var boundaries = new List<BoundaryIncoming>();

        //_ = await csv.ReadAsync().ConfigAwait();
        //_ = csv.ReadHeader();
        //while (await csv.ReadAsync().ConfigAwait())
        //{
        //    if (csv.TryGetField<int>("CSDUID", out var csduid)
        //        && csv.TryGetField<string>("WKT", out var wkt)
        //        && csv.TryGetField<string>("CSDNAME", out var csdname)
        //        && csv.TryGetField<int>("PRUID", out var prid)
        //        )
        //    {
        //        boundaries.Add(new BoundaryIncoming
        //        {
        //            Id = csduid,
        //            BoundaryWellKnownText = wkt,
        //            SubdivisionName = csdname,
        //            Province = string.Empty,
        //        });
        //    }
        //}
        var boundaries = ReadBoundaries(filenameAndPath);

        await spuriousRepository.ImportBoundaries(boundaries).ConfigAwait();
        //return boundaries.ToL;
    }

    private static async IAsyncEnumerable<BoundaryIncoming> ReadBoundaries(string filenameAndPath)
    {
        using var reader = new StreamReader(filenameAndPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        _ = await csv.ReadAsync().ConfigAwait();
        _ = csv.ReadHeader();
        while (await csv.ReadAsync().ConfigAwait())
        {
            if (csv.TryGetField<int>("CSDUID", out var csduid)
                && csv.TryGetField<string>("WKT", out var wkt)
                && csv.TryGetField<string>("CSDNAME", out var csdname)
                && csv.TryGetField<int>("PRUID", out var prid)
                )
            {
                yield return new BoundaryIncoming
                {
                    Id = csduid,
                    BoundaryWellKnownText = wkt,
                    SubdivisionName = csdname,
                    Province = string.Empty,
                };
            }
        }
    }

    public async Task<IEnumerable<PopulationIncoming>> ImportPopulationFrom98File(string filenameAndPath)
    {
        // First pass is to extract province/territory names and IDs
        var provincesDict = new Dictionary<int, string>();
        using var provinceReader = new StreamReader(filenameAndPath, Encoding.UTF8);
        using var provinceCsv = new CsvReader(provinceReader, CultureInfo.InvariantCulture);
        {

            _ = await provinceCsv.ReadAsync().ConfigAwait();
            _ = provinceCsv.ReadHeader();
            while (await provinceCsv.ReadAsync().ConfigAwait())
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

        _ = await csv.ReadAsync().ConfigAwait();
        _ = csv.ReadHeader();
        while (await csv.ReadAsync().ConfigAwait())
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
                    records.Add(new PopulationIncoming
                    {
                        Id = id,
                        Population = population,
                        Province = provincesDict[provinceId]
                    });
                }
            }
        }

        await spuriousRepository.ImportPopulations(records).ConfigAwait();
        return records;
    }
}
