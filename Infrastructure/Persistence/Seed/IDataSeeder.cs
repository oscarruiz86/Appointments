using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Persistence.Seed
{
    public interface IDataSeeder
    {
        Task SeedAsync(IApplicationBuilder app);
    }
}
