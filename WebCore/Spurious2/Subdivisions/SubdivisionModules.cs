//using AutoMapper;
using Carter;
using MediatR;
using Spurious2.Core2;
using Spurious2.Core2.Subdivisions;

namespace Spurious2.Subdivisions;

public class SubdivisionModules : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app) => app.MapGet("/api/densities/{name}/subdivisions",
            async (string name, ISender mediator, //IMapper mapper,
                                                  CancellationToken cancellationToken) =>

            (await mediator.Send(new GetSubdivisionsByDensityRequest { DensityName = name }, cancellationToken)
                    .ConfigAwait()).Select(s => s.ToSubdivision()).ToList())

            .WithTags("Subdivisions")
            .WithName("GetSubdivisionsByDensity")
            ;
}
