
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Tenancy
{

    public static class TenancyExtensions
    {
        public static IServiceCollection AddTenancy(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ITenantProvider, TenantProvider>();

            return services;
        }

        public static IApplicationBuilder UseTenancy(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TenantMiddleware>();
        }
    }
}
