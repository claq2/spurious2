using System.Globalization;
using System.Runtime.CompilerServices;
using CsvHelper;

namespace Spurious2.Core2.Subdivisions;

public class SubdivisionImportingService(ISpuriousRepository spuriousRepository) : ISubdivisionImportingService
{
    public async Task ImportBoundaryFromCsvFile(string filenameAndPath, CancellationToken cancellationToken)
    {
        static async IAsyncEnumerable<BoundaryIncoming> ReadBoundaries2(CsvReader boundariesReader, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await foreach (var record in boundariesReader.GetRecordsAsync<BoundaryIncoming>(cancellationToken).ConfigureAwait(false))
            {
                yield return record;
            }
        }

        using var boundariesReader = CsvReader.FromFile(filenameAndPath);
        var boundaries = ReadBoundaries2(boundariesReader, cancellationToken);

        await spuriousRepository.ClearBoundaryIncoming().ConfigAwait();
        await Parallel.ForEachAsync(boundaries, async (b, ct) => await spuriousRepository.ImportBoundary(b, ct).ConfigAwait())
            .ConfigAwait();
        await spuriousRepository.UpdateBoundariesFromIncoming().ConfigAwait();
    }

    public async Task ImportPopulationFrom98File(string filenameAndPath, CancellationToken cancellationToken)
    {
        static async IAsyncEnumerable<PopulationIncoming> ReadPopulations2(CsvReader populationReader, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var provincesDict = new Dictionary<int, string>();
            await foreach (var record in populationReader.GetRecordsAsync<PopulationIncoming>(cancellationToken).ConfigureAwait(false))
            {
                if (record.GeoLevel is "Province" or "Territory")
                {
                    provincesDict[record.Id] = record.GeoName ?? string.Empty;
                }

                if (record.GeoLevel is "Census subdivision" or "Census division" or "Province" or "Territory")
                {
                    var first2Chars = record.Id.ToString(CultureInfo.InvariantCulture)[..2];
                    record.Province = provincesDict.TryGetValue(int.Parse(first2Chars, CultureInfo.InvariantCulture), out var provinceName) ? provinceName : string.Empty;
                    yield return record;
                }
            }
        }

        using var populationReader = CsvReader.FromFile(filenameAndPath);
        var records2 = ReadPopulations2(populationReader, cancellationToken);

        await spuriousRepository.ClearPopulationIncoming().ConfigAwait();
        await Parallel.ForEachAsync(records2, cancellationToken, async (b, ct) => await spuriousRepository.ImportPopulation(b).ConfigAwait())
            .ConfigAwait();
        await spuriousRepository.UpdatePopulationsFromIncoming().ConfigAwait();
    }
}
