using Ardalis.Specification;
using NetTopologySuite.Geometries;

namespace Spurious2.Core2.Subdivisions;

public class BoundryBySubdivisionSpec : SingleResultSpecification<Subdivision, Geometry?>
{
    public BoundryBySubdivisionSpec(int subdivisionId)
    {
        this.Query.Where(s => s.Id == subdivisionId).Select(s => s.Boundary);
    }
}
