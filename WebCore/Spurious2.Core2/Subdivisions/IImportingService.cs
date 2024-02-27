using Spurious2.Core.Boundaries;
using Spurious2.Core.Populations;

namespace Spurious2.Core.SubdivisionImporting.Services;

public interface IImportingService : IDisposable
{
    IEnumerable<PopulationIncoming> ImportPopulationFrom98File(string filenameAndPath);
    IEnumerable<BoundaryIncoming> ImportBoundaryFromCsvFile(string filenameAndPath);
    //int ImportPopulationFromFile(string filenameAndPath);
    //int ImportBoundaryFromGmlFile(string filenameAndPath);
}
