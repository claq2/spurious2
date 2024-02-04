using AutoMapper;
using Carter;
using MediatR;
using Spurious2.Core2.Stores;

namespace Spurious2.Stores;

public class StoresModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        _ = app.MapGet("/subdivisions/{id}/stores",
            async (int id, ISender mediator, IMapper mapper) =>
                mapper.Map<List<Store>>(
                    await mediator.Send(
                        new GetStoresForSubdivisionRequest { SubdivisionId = id })))
            .WithTags("Stores")
            .WithName("GetSubdivisionStores")
            .WithOpenApi();
    }
}
