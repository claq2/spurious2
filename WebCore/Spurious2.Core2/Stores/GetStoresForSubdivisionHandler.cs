using MediatR;

namespace Spurious2.Core2.Stores;

public class GetStoresForSubdivisionHandler : IRequestHandler<GetStoresForSubdivisionRequest, List<Store>>
{
    public Task<List<Store>> Handle(GetStoresForSubdivisionRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
