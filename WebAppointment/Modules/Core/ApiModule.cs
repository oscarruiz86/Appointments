
namespace WebApi.Modules.Core
{
    public static class ApiModule
    {
        public static IServiceCollection AddApiModule(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddOpenApi();

            return services;
        }
    }
}
