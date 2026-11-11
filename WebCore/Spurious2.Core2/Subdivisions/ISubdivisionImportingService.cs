namespace Spurious2.Core2.Subdivisions;

public interface ISubdivisionImportingService
{
    //public Task ImportBoundaryFromCsvFileBulk(string filenameAndPath);
    public Task ImportPopulationFrom98File(string filenameAndPath, CancellationToken cancellationToken);
    public Task ImportBoundaryFromCsvFile(string filenameAndPath, CancellationToken cancellationToken);
}
