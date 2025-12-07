namespace Spurious2.Core2.Subdivisions;

public interface ISubdivisionInMemImportingService
{
#pragma warning disable CA1002 // Do not expose generic lists
    public List<Subdivision> ImportWithData();
#pragma warning restore CA1002 // Do not expose generic lists
}
