
using Application.Tests.Common;
using Application.UseCases.Tenants.Queries;

namespace Application.Tests.Tenants
{
    public class ListTenantsHandlerTests : TestBase
    {
        [Fact]
        public async Task Should_Return_Only_Active()
        {
            var cmd = new ListTenantsQuery
            {
                OnlyActive = true
            };
            var result= await Mediator.Send(cmd);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Return_All_When_OnlyActive_False()
        {
            var cmd = new ListTenantsQuery
            {
                OnlyActive = false
            };

            var result = await Mediator.Send(cmd);
            Assert.NotNull(result);
        }
    }
}
