using Spurious2.Core2.Subdivisions;

namespace Spurious2.Core.SubdivisionImporting.Services;

public interface ISubdivisionImportingService : IDisposable
{
    Task<IEnumerable<PopulationIncoming>> ImportPopulationFrom98File(string filenameAndPath);
    Task<IEnumerable<BoundaryIncoming>> ImportBoundaryFromCsvFile(string filenameAndPath);
    //int ImportPopulationFromFile(string filenameAndPath);
    //int ImportBoundaryFromGmlFile(string filenameAndPath);
}
