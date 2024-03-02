namespace Spurious2.Core.SubdivisionImporting.Services;

public interface ISubdivisionImportingService : IDisposable
{
    Task ImportPopulationFrom98File(string filenameAndPath);
    Task ImportBoundaryFromCsvFile(string filenameAndPath);
}
