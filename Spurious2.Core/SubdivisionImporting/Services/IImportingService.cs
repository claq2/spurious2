namespace Spurious2.Core.SubdivisionImporting.Services;

public interface IImportingService : IDisposable
{
    int ImportBoundaryFromCsvFile(string filenameAndPath);
    int ImportPopulationFromFile(string filenameAndPath);
    int ImportBoundaryFromGmlFile(string filenameAndPath);
}
