using MediatR;

namespace Spurious2.Core2.Stores;

public class GetStoresForSubdivisionHandler(ISpuriousService spuriousService) : IRequestHandler<GetStoresForSubdivisionRequest, List<Store>>
{
    public async Task<List<Store>> Handle(GetStoresForSubdivisionRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var stores = await spuriousService
            .GetStoresBySubdivisionId(request.SubdivisionId, cancellationToken)
            .ConfigAwait();
        return stores;
    }
}
