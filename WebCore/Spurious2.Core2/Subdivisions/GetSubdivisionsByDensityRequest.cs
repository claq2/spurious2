using MediatR;

namespace Spurious2.Core2.Subdivisions;

public record GetSubdivisionsByDensityRequest : IRequest<List<Subdivision>>
{
    public required string DensityName { get; init; }
}
