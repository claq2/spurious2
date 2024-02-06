using MediatR;
using Spurious2.Core;

namespace Spurious2.Core2.Stores;

public class GetStoresForSubdivisionHandler(ISpuriousRepository spuriousRepository) : IRequestHandler<GetStoresForSubdivisionRequest, List<Store>>
{
    public async Task<List<Store>> Handle(GetStoresForSubdivisionRequest request, CancellationToken cancellationToken)
    {
        var stores = await spuriousRepository
            .GetStoresBySubdivisionId(request.SubdivisionId)
            .ConfigAwait();
        return stores;
    }
}
