using MediatR;

namespace Spurious2.Core2.Stores;

public class GetStoresForSubdivisionRequest : IRequest<List<Store>>
{
    public required int SubdivisionId { get; init; }
}
