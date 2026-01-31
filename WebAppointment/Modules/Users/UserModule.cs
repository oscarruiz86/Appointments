using Application.UseCases.Users.Handlers;

namespace WebApi.Modules.Users
{
    public static class UserModule
    {
        public static IServiceCollection AddUserModule(
            this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<CreateUserHandler>()
                .AddClasses(c => c.Where(t =>
                    t.Name.EndsWith("Handler")))
                .AsSelf()
                .WithScopedLifetime());

            return services;
        }
    }
}
