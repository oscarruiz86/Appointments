using Application.Common.Rules;

namespace WebApi.Modules.Rules
{
    public static class RuleModule
    {
        public static IServiceCollection AddRuleModule(
            this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<IRule>()
                .AddClasses(c => c.AssignableTo<IStaticRule>())
                .AsSelf()
                .WithScopedLifetime());

            services.AddScoped<RuleFactory>();

            return services;
        }
    }
}
