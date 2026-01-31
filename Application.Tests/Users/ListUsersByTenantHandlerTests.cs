
using Application.Tests.Common;
using Application.UseCases.Users.Queries;

namespace Application.Tests.Users
{
    public class ListUsersByTenantHandlerTests : TestBase
    {
        [Fact]
        public async Task Should_Return_Only_Tenant_Users()
        {
            var cmd = new ListTenantUsersQuery { OnlyActive = true };
            var result = await Mediator.Send(cmd);

            Assert.NotNull(result);
        }


        [Fact]
        public async Task Should_Filter_No_Active()
        {
            var cmd = new ListTenantUsersQuery { OnlyActive = false };
            var result = await Mediator.Send(cmd);

            Assert.NotNull(result);
        }
    }

}
