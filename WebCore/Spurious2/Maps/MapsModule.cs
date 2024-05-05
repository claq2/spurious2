using Azure.Core;
using Azure.Identity;
using Carter;
using Spurious2.Core2;

namespace Spurious2.Maps;

public class MapsModule : ICarterModule
{
    private static readonly DefaultAzureCredential tokenProvider = new();
    public void AddRoutes(IEndpointRouteBuilder app) => app.MapGet("/api/azure-maps-token",
        async (CancellationToken cancellationToken) =>
        {
            var accessToken = await tokenProvider.GetTokenAsync(
                new TokenRequestContext(["https://atlas.microsoft.com/.default"]), cancellationToken
            ).ConfigAwait();
            return accessToken.Token;
        });
}
