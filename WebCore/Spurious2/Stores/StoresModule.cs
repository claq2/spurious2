using Carter;

namespace Spurious2.Stores;

public class StoresModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        _ = app.MapGet("/subdivisions/{id}/stores",
            (int id) => { return id; });
    }
}
