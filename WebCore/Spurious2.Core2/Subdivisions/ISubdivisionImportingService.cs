namespace Spurious2.Core2.Subdivisions;

public interface ISubdivisionImportingService : IDisposable
{
#pragma warning disable CA1002 // Do not expose generic lists
    public List<Subdivision> ImportWithData();
#pragma warning restore CA1002 // Do not expose generic lists
    public Task ImportBoundaryFromCsvFileBulk(string filenameAndPath);
    public Task ImportPopulationFrom98File(string filenameAndPath);
    public Task ImportBoundaryFromCsvFile(string filenameAndPath);
}
