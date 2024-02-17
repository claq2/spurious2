using Carter;
using MediatR;
using Spurious2.Core2;
using Spurious2.Core2.Densities;

namespace Spurious2.Densities;

public class DensitiesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app) => app.MapGet("/api/densities",
            async (ISender mediator) =>
                await mediator.Send(new GetDensitiesRequest()).ConfigAwait())
            .WithTags("Densities")
            .WithName("GetDensities")
            .WithOpenApi();
}
