

using Application.Common.Exceptions;
using Application.Tests.Common;
using Application.Tests.Common.Fake;
using Application.UseCases.Tenants.Commands;

namespace Application.Tests.Tenants
{
    public class DisableTenantHandlerTests : TestBase
    {
        [Fact]
        public async Task Should_Disable_Tenant()
        {
            var cmd = new DisableTenantCommand
            {
                Id = FakeIds.TenantId
            };

            await Mediator.Send(cmd);
        }

        [Fact]
        public async Task Should_Throw_When_Tenant_Not_Found()
        {
            var cmd = new DisableTenantCommand
            {
                Id = Guid.NewGuid()
            };

            await Assert.ThrowsAsync<BusinessRuleException>(async () =>
            {
                await Mediator.Send(cmd);
            });
        }

    }
}
