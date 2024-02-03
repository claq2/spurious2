using MediatR;

namespace Spurious2.Core2.Boundaries;

public record GetBoundaryForSubdivisionRequest : IRequest<string>
{
    public required int SubdivisionId { get; init; }
}
