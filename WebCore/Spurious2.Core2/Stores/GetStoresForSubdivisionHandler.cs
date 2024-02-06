using MediatR;

namespace Spurious2.Core2.Stores;

public class GetStoresForSubdivisionHandler(ISpuriousRepository spuriousRepository) : IRequestHandler<GetStoresForSubdivisionRequest, List<Store>>
{
    public async Task<List<Store>> Handle(GetStoresForSubdivisionRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(nameof(request));
        var stores = await spuriousRepository
            .GetStoresBySubdivisionId(request.SubdivisionId, cancellationToken)
            .ConfigAwait();
        return stores;
    }
}
