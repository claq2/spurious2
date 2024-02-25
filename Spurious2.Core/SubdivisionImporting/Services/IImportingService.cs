namespace Spurious2.Core.SubdivisionImporting.Services;

public interface IImportingService : IDisposable
{
    int ImportPopulationFrom98File(string filenameAndPath);
    int ImportBoundaryFromCsvFile(string filenameAndPath);
    int ImportPopulationFromFile(string filenameAndPath);
    int ImportBoundaryFromGmlFile(string filenameAndPath);
}
