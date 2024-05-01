using AutoMapper;
using Carter;
using MediatR;
using Spurious2.Core2;
using Spurious2.Core2.Stores;

namespace Spurious2.Stores;

public class StoresModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app) => app.MapGet("api/subdivisions/{id}/stores",
            async (int id, ISender mediator, IMapper mapper, CancellationToken cancellationToken) =>
                mapper.Map<List<Store>>(
                    await mediator.Send(
                        new GetStoresForSubdivisionRequest { SubdivisionId = id }, cancellationToken)
                    .ConfigAwait()))
            .WithTags("Stores")
            .WithName("GetSubdivisionStores")
            .WithOpenApi();
}
