using Domain.Entities.Identity;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Seed
{
    public sealed class IdentitySeeder : IDataSeeder
    {
        public static readonly Guid AdminUserId = Guid.NewGuid();

        public async Task SeedAsync(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            var db = services.GetRequiredService<ApplicationDbContext>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
            // Roles
            const string adminRole = "Admin";
            var roles = new[]
                    {
                       adminRole,
                       "TenantAdmin",
                       "Employee",
                       "Customer"
                    };

            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new ApplicationRole
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpperInvariant()
                    });
                }
            }

            // Usuario Admin
            const string emailAdmin = "admin@system.com";
            var adminExist = await db.Users.IgnoreQueryFilters().AnyAsync(u => u.Email == emailAdmin);
            if (!adminExist)
            {
                var admin = new ApplicationUser
                {
                    Id = AdminUserId,
                    UserName = emailAdmin,
                    Email = emailAdmin,
                    EmailConfirmed = true,
                    IsActive = true,
                    IsDeleted = false,
                    TenantId = TenantSeeder.DefaultTenantId
                };

                var result = await userManager.CreateAsync(admin, "Admin123*");
            }
                       

            var exists = await db.UserRoles.IgnoreQueryFilters()
                .AnyAsync(ur => ur.User.Email == emailAdmin);

            if (!exists)
            {
                var adminRoleEntity = await roleManager.FindByNameAsync(adminRole);
                var userRole = new UserRole
                {
                    UserId = AdminUserId,
                    RoleId = adminRoleEntity.Id,
                    TenantId = TenantSeeder.DefaultTenantId
                };

                db.UserRoles.Add(userRole);
                await db.SaveChangesAsync();
            }
        }
    }

}
