using Application.Tests.Common.Fake;
using Domain.Entities;
using Domain.Entities.Identity;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Identity;

namespace Application.Tests.Common
{
    public static class TestDataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext ctx)
        {
            var tenant = CreateTenant();
            ctx.Tenants.Add(tenant);

            var role = CreateAdminRole();
            ctx.Roles.Add(role);

            var user = CreateUser(tenant.Id, FakeIds.UserId);
            ctx.Users.Add(user);

            var user2 = CreateUser(tenant.Id, FakeIds.UserId2,false);
            ctx.Users.Add(user2);

            var user3 = CreateUser(tenant.Id, FakeIds.UserId3);
            ctx.Users.Add(user3);

            var userRole = CreateUserRole(user.Id, role.Id);
            ctx.UserRoles.Add(userRole);

            await ctx.SaveChangesAsync();
        }

        // ===== FACTORIES =====

        public static Tenant CreateTenant()
        {
            return new Tenant
            {
                Id = FakeIds.TenantId,
                Name = FakeConstants.TestTenantName,
                Phone = "3000000000",
                Timezone = "America/Bogota",
                IsActive = true
            };
        }

        public static ApplicationRole CreateAdminRole()
        {
            return new ApplicationRole
            {
                Id = FakeIds.AdminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            };
        }

        public static ApplicationUser CreateUser(Guid tenantId, Guid Id, bool active = true)
        {
            var user = new ApplicationUser
            {
                Id = Id,
                UserName = FakeConstants.TestUserName,
                NormalizedUserName = FakeConstants.TestUserName.ToUpper(),
                Email = FakeConstants.TestEmail,
                NormalizedEmail = FakeConstants.TestEmail.ToUpper(),
                IsActive = active,
                TenantId = tenantId,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            var hasher = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = hasher.HashPassword(user, FakeConstants.TestPassword);

            return user;
        }

        public static UserRole CreateUserRole(Guid userId, Guid roleId)
        {
            return new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };
        }
    }
}
