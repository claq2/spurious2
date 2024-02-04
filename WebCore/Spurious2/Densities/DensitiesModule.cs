using Carter;
using MediatR;
using Spurious2.Core2.Densities;

namespace Spurious2.Densities;

public class DensitiesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        _ = app.MapGet("/api/densities",
            async (ISender mediator) => await mediator.Send(new GetDensitiesRequest()))
            .WithTags("Densities")
            .WithName("GetDensities")
            .WithOpenApi();
    }
}
