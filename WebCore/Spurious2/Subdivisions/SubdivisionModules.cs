using Carter;
using MediatR;
using Spurious2.Core2.Subdivisions;

namespace Spurious2.Subdivisions;

public class SubdivisionModules : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        _ = app.MapGet("/api/densities/{name}/subdivisions",
            async (string name, ISender mediator) =>
            await mediator.Send(new GetSubdivisionsByDensityRequest { DensityName = name }))
            .WithTags("Subdivisions")
            .WithName("GetSubdivisionsByDensity")
            .WithOpenApi()
            ;
        // TODO: Map to subdiv DTO
    }
}
