using MediatR;
using Spurious2.Core;

namespace Spurious2.Core2.Boundaries;

public class GetBoundaryForSubdivisionHandler(ISpuriousRepository spuriousRepository) : IRequestHandler<GetBoundaryForSubdivisionRequest, string>
{
    public async Task<string> Handle(GetBoundaryForSubdivisionRequest request, CancellationToken cancellationToken)
    {
        var boundary = await spuriousRepository.GetBoundaryForSubdivision(request.SubdivisionId);
        return boundary;
    }
}
