namespace Spurious2.Core.SubdivisionImporting.Domain;

public interface ISubdivisionRepository : IDisposable
{
    Task UpdateSubdivisionVolumes();
    void Import(IEnumerable<SubdivisionPopulation> populations);
    void Import(IEnumerable<SubdivisionBoundary> boundaries);
}
