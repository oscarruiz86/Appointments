using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Seed;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Modules.Persistence
{
    public static class PersistenceSeederModule
    {
        /// <summary>
        /// Ejecuta migraciones automáticas y pobla datos iniciales.
        /// Solo en entorno de desarrollo.
        /// </summary>
        public static async Task ApplyMigrationsAndSeedAsync(this WebApplication app)
        {

            if (!app.Environment.IsDevelopment())
            {
                return; // 🔹 Solo ejecuta en desarrollo
            }

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<WebApplication>>();

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();

                // 🔹 Migraciones automáticas
                logger.LogInformation("Aplicando migraciones de base de datos (Desarrollo)...");
                await context.Database.MigrateAsync();

                // 🔹 Seeding inicial
                logger.LogInformation("Poblando datos iniciales (Desarrollo)...");
                await ApplicationDbSeeder.SeedAsync(app);

                logger.LogInformation("Migraciones y seeding completados (Desarrollo).");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al aplicar migraciones o poblar datos iniciales (Desarrollo)");
                throw;
            }
        }
    }
}
