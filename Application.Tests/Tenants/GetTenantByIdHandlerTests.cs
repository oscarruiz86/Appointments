
using Application.Tests.Common;
using Application.Tests.Common.Fake;
using Application.UseCases.Tenants.Queries;

namespace Application.Tests.Tenants
{
    public class GetTenantByIdHandlerTests : TestBase
    {

        [Fact]
        public async Task Should_Return_Tenant_With_Employees()
        {
            var cmd = new GetTenantByIdQuery
            {
                Id = FakeIds.TenantId
            };
            await Mediator.Send(cmd);
        }


        [Fact]
        public async Task Should_Return_Null_When_Not_Found()
        {
           var cmd = new GetTenantByIdQuery
           {
               Id = Guid.NewGuid()
           };
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await Mediator.Send(cmd);
            });
        }
    }
}
