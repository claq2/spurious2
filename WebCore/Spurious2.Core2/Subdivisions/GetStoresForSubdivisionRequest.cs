using MediatR;

namespace Spurious2.Core2.Subdivisions;

public class GetStoresForSubdivisionRequest : IRequest<List<Store>>
{
    public required int SubdivisionId { get; init; }
}
