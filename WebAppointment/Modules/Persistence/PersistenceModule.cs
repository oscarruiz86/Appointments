using Application.Interfaces.Persistence;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Filters;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces.Persistence.Filters;

namespace WebApi.Modules.Persistence
{
    public static class PersistenceModule
    {
        public static IServiceCollection AddPersistenceModule(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    config.GetConnectionString("DefaultConnection"),
                    b =>
                    {
                        b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                        b.MigrationsHistoryTable("__EFMigrationsHistory", "public");
                    }));

            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFilterContext, FilterContext>();

            return services;
        }
    }
}
