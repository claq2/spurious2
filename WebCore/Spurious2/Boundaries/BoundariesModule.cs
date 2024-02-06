using Carter;
using MediatR;
using Spurious2.Core2;
using Spurious2.Core2.Boundaries;

namespace Spurious2.Boundaries;

public class BoundariesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        _ = app.MapGet("/subdivisions/{id}/boundary",
            async (int id, ISender mediator) =>
                Results.Text(await mediator
                    .Send(new GetBoundaryForSubdivisionRequest
                    { SubdivisionId = id })
                    .ConfigAwait(), contentType: "application/json"))
            .WithTags("Boundaries")
            .WithName("GetSubdivisionBoundary")
            .WithOpenApi()
            ;
    }
}
