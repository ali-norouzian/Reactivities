
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions
{
    public static class MigrateAndSeedDbExtensions
    {
        public static async Task<WebApplication> MigrateAndSeedDb(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DataContext>();

                await context.Database.MigrateAsync();
                await Seed.SeedData(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();

                logger.LogError(ex, "An error occured during migration database.");
            }

            return app;
        }
    }
}