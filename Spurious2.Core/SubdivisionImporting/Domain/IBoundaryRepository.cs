namespace Spurious2.Core.SubdivisionImporting.Domain;

public interface IBoundaryRepository : IDisposable
{
    void Import(IEnumerable<SubdivisionBoundary> boundaries);
}
