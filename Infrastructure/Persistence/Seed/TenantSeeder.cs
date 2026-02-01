
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Seed
{
    public sealed class TenantSeeder : IDataSeeder
    {
        public static readonly Guid DefaultTenantId = Guid.NewGuid();

        public async Task SeedAsync(IApplicationBuilder app)
        {

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            var db = services.GetRequiredService<ApplicationDbContext>();

            // ==============================
            // Crear Tenant
            // ==============================
            var tenant = await db.Tenants
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(t => t.Id == DefaultTenantId);

            if (tenant == null)
            {
                tenant = new Tenant
                {
                    Id = DefaultTenantId,
                    Name = "Default Tenant",
                    IsActive = true,
                    IsDeleted = false
                };

                db.Tenants.Add(tenant);
                await db.SaveChangesAsync();
            }
        }
    }

}
