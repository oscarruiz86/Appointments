using Infrastructure.Persistence.Context;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Tests.Common
{
    public abstract class TestBase
    {
        private readonly ServiceProvider _provider;

        protected IServiceScope Scope { get; }
        protected IMediator Mediator { get; }

        protected TestBase()
        {
            _provider = TestServiceFactory.Create();

            Scope = _provider.CreateScope();

            var ctx = Scope.ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

            TestDataSeeder.SeedAsync(ctx).Wait();

            Mediator = Scope.ServiceProvider.GetRequiredService<IMediator>();

        }

        protected T Get<T>()
            => Scope.ServiceProvider.GetRequiredService<T>();
    }
}
