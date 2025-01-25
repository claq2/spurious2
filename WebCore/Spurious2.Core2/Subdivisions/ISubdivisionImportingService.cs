namespace Spurious2.Core2.Subdivisions;

public interface ISubdivisionImportingService : IDisposable
{
    Task ImportBoundaryFromCsvFileBulk(string filenameAndPath);
    Task ImportPopulationFrom98File(string filenameAndPath);
    Task ImportBoundaryFromCsvFile(string filenameAndPath);
}
