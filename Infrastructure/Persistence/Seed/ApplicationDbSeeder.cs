
using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Persistence.Seed
{
    public static class ApplicationDbSeeder
    {
        public static async Task SeedAsync(IApplicationBuilder app)
        {

            var seeders = new IDataSeeder[]
            {
                new TenantSeeder(),
                new IdentitySeeder()
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(app);
            }
        }
    }
}
