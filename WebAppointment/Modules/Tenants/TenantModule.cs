using Application.Common.Rules;
using Application.UseCases.Tenants.Handlers;

namespace WebApi.Modules.Tenants
{
    public static class TenantModule
    {
        public static IServiceCollection AddTenantModule(
            this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<CreateTenantHandler>()
                .AddClasses(c => c.Where(t =>
                    t.Name.EndsWith("Handler")))
                .AsSelf()
                .WithScopedLifetime());

            return services;
        }
    }
}
