namespace Spurious2.Core2.Subdivisions;

public interface ISubdivisionImportingService : IDisposable
{
    public Task ImportBoundaryFromCsvFileBulk(string filenameAndPath);
    public Task ImportPopulationFrom98File(string filenameAndPath);
    public Task ImportBoundaryFromCsvFile(string filenameAndPath);
}
