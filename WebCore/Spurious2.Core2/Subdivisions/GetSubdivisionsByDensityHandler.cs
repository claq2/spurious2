using MediatR;

namespace Spurious2.Core2.Subdivisions
{
    public class GetSubdivisionsByDensityHandler : IRequestHandler<GetSubdivisionsByDensityRequest, List<Subdivision>>
    {
        public Task<List<Subdivision>> Handle(GetSubdivisionsByDensityRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
