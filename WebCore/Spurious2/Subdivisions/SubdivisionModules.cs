using Carter;

namespace Spurious2.Subdivisions;

public class SubdivisionModules : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        _ = app.MapGet("/api/densities/{name}/subdivisions", (string name) => { return name; });

        _ = app.MapGet("/subdivisions/{id}/boundary", (int id) => { return ""; });

        _ = app.MapGet("/subdivisions/{id}/stores", (int id) => { return id; });
    }
}
