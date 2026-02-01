
using Application.Common.Behaviors;
using Application.Common.Rules;
using Application.Interfaces.Infrastructure.Services;
using Application.Interfaces.Infrastructure.Tenancy;
using Application.Interfaces.Persistence;
using Application.Interfaces.Persistence.Filters;
using Application.Tests.Common.Fake;
using Application.UseCases.Users.Commands;
using Application.UseCases.Users.Validators;
using Domain.Entities.Identity;
using FluentValidation;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Filters;
using Infrastructure.Tenancy;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;


namespace Application.Tests.Common
{
    public static class TestServiceFactory
    {
        public static ServiceProvider Create()
        {
            var services = new ServiceCollection();

            // =========================
            // Db InMemory
            // =========================
            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            // =========================
            // Fake tenancy
            // =========================
            services.AddScoped<ITenantContext, FakeTenantContext>();
            services.AddScoped<ICurrentUserService, FakeCurrentUserService>();
            services.AddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();

            var fakeTenantProvider = new FakeTenantProvider
            {
                TenantId = FakeIds.TenantId
            };

            services.AddScoped<ITenantProvider>(_ => fakeTenantProvider);

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly);
            });

            services.AddValidatorsFromAssembly(typeof(CreateUserValidator).Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddScoped<IFilterContext, FilterContext>();

            // =========================
            // MediatR
            // =========================
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<Application.AssemblyReference>();
            });

            // =========================
            // Rules
            // =========================
            services.Scan(scan => scan
                .FromAssemblyOf<IRule>()
                .AddClasses(c => c.AssignableTo<IStaticRule>())
                .AsSelf()
                .WithScopedLifetime());

            services.AddScoped<RuleFactory>();

            return services.BuildServiceProvider();
        }
    }
}
