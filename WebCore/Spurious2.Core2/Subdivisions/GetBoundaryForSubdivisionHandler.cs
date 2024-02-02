using MediatR;

namespace Spurious2.Core2.Subdivisions;

public class GetBoundaryForSubdivisionHandler : IRequestHandler<GetBoundaryForSubdivisionRequest, string>
{
    public Task<string> Handle(GetBoundaryForSubdivisionRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult("");
    }
}
