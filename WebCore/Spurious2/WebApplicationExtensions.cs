using Microsoft.EntityFrameworkCore;
using Spurious2.Core2;

namespace Spurious2;

public static class WebApplicationExtensions
{
    public static async Task<WebApplication> MigrateDatabase<T>(this WebApplication webHost) where T : DbContext
    {
        ArgumentNullException.ThrowIfNull(webHost);
        using (var scope = webHost.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
#pragma warning disable CA1031 // Do not catch general exception types
            try
            {
                var db = services.GetRequiredService<T>();
                await db.Database.MigrateAsync().ConfigAwait();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.MigrationError(ex);
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        return webHost;
    }
}
