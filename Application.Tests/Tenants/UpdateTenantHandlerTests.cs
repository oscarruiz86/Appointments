
using Application.Tests.Common;
using Application.Tests.Common.Fake;
using Application.UseCases.Tenants.Commands;

namespace Application.Tests.Tenants
{
    public class UpdateTenantHandlerTests : TestBase
    {
        [Fact]
        public async Task Should_Update_Tenant()
        {
            var cmd = new UpdateTenantCommand
            {
                Id = FakeIds.TenantId,
                Name = "New",
                Phone = "+34600123456",
                Timezone = "UTC"
            };
            await Mediator.Send(cmd);
        }


        [Fact]
        public async Task Should_Throw_When_Tenant_Not_Found()
        {
            var cmd = new UpdateTenantCommand
            {
                Id = Guid.NewGuid(),
                Name = "New",
                Phone = "+34600123456",
                Timezone = "UTC"
            };
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await Mediator.Send(cmd);
            });
        }
    }

}
