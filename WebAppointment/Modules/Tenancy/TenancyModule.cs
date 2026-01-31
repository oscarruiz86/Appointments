using Application.Interfaces.Infrastructure.Services;
using Application.Interfaces.Infrastructure.Tenancy;
using Infrastructure.Services;
using Infrastructure.Tenancy;

namespace WebApi.Modules.Tenancy
{
    public static class TenancyModule
    {
        public static IServiceCollection AddTenancyModule(
            this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<ITenantContext, TenantContext>();
            services.AddScoped<ITenantProvider, TenantProvider>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
