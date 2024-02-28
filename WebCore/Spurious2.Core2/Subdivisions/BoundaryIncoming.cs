using NetTopologySuite.Geometries;

namespace Spurious2.Core2.Subdivisions;

public partial class BoundaryIncoming
{
    public int Id { get; set; }

    public string? BoundaryWellKnownText { get; set; }

    public string SubdivisionName { get; set; } = null!;

    public string Province { get; set; } = null!;

    public Geometry? OriginalBoundary { get; set; }

    public Geometry? ReorientedBoundary { get; set; }
}
