using Carter;
using MediatR;
using Spurious2.Core2;
using Spurious2.Core2.Densities;

namespace Spurious2.Densities;

public class DensitiesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        _ = app.MapGet("/api/densities",
            async (ISender mediator, CancellationToken cancellationToken) =>
                await mediator.Send(new GetDensitiesRequest(), cancellationToken).ConfigAwait())
            .WithTags("Densities")
            .WithName("GetDensities")
            .WithOpenApi();
    }
}
