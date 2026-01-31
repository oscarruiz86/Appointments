using Application.Common.Behaviors;
using Application.Common.Tenancy;
using Application.UseCases.Users.Handlers;
using Application.UseCases.Users.Validators;
using FluentValidation;
using MediatR;

namespace WebApi.Modules.Core
{
    public static class CoreModule
    {
        public static IServiceCollection AddApplicationCoreModule(
            this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(ChangePasswordHandler).Assembly));

            services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TenantGuardBehavior<,>));

            return services;
        }
    }
}
