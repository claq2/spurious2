using Spurious2.Core.Boundaries;

namespace Spurious2.Core.SubdivisionImporting.Services;

public interface IImportingService : IDisposable
{
    int ImportPopulationFrom98File(string filenameAndPath);
    IEnumerable<BoundaryIncoming> ImportBoundaryFromCsvFile(string filenameAndPath);
    //int ImportPopulationFromFile(string filenameAndPath);
    //int ImportBoundaryFromGmlFile(string filenameAndPath);
}
