using MediatR;

namespace Spurious2.Core2.Subdivisions;

public class GetBoundaryForSubdivisionHandler(ISpuriousRepository spuriousRepository) : IRequestHandler<GetBoundaryForSubdivisionRequest, string>
{
    public async Task<string> Handle(GetBoundaryForSubdivisionRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var boundary = await spuriousRepository
            .GetBoundaryForSubdivision(request.SubdivisionId, cancellationToken)
            .ConfigAwait();
        return boundary;
    }
}
