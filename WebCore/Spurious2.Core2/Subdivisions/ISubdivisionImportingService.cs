namespace Spurious2.Core.SubdivisionImporting.Services;

public interface ISubdivisionImportingService : IDisposable
{
    Task ImportBoundaryFromCsvFileBulk(string filenameAndPath);
    Task ImportPopulationFrom98File(string filenameAndPath);
    Task ImportBoundaryFromCsvFile(string filenameAndPath);
}
