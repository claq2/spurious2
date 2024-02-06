using AutoMapper;
using Carter;
using MediatR;
using Spurious2.Core2;
using Spurious2.Core2.Subdivisions;

namespace Spurious2.Subdivisions;

public class SubdivisionModules : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        _ = app.MapGet("/api/densities/{name}/subdivisions",
            async (string name, ISender mediator, IMapper mapper) =>
                mapper.Map<List<Subdivision>>(
                    await mediator.Send(new GetSubdivisionsByDensityRequest { DensityName = name })
                    .ConfigAwait()))
            .WithTags("Subdivisions")
            .WithName("GetSubdivisionsByDensity")
            .WithOpenApi()
            ;
    }
}
