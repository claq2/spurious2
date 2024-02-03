using Carter;

namespace Spurious2.Boundaries;

public class BoundariesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        _ = app.MapGet("/subdivisions/{id}/boundary",
            (int id) => { return ""; });
    }
}
