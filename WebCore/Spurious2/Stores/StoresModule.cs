//using AutoMapper;
using Carter;
using MediatR;
using Spurious2.Core2;
using Spurious2.Core2.Stores;

namespace Spurious2.Stores;

public class StoresModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app) => app.MapGet("api/subdivisions/{id}/stores",
            async (int id, ISender mediator,
                //IMapper mapper,
                CancellationToken cancellationToken) =>
            (await mediator.Send(
                        new GetStoresForSubdivisionRequest { SubdivisionId = id }, cancellationToken)
                    .ConfigAwait()).Select(s => s.ToStore()).ToList())
            .WithTags("Stores")
            .WithName("GetSubdivisionStores");
}
