
using Application.Common.Exceptions;
using Application.Tests.Common;
using Application.Tests.Common.Fake;
using Application.UseCases.Users.Queries;

namespace Application.Tests.Users
{
    public class GetUserByIdHandlerTests : TestBase
    {
        [Fact]
        public async Task Should_Return_User()
        {            
           var cmd = new GetUserByIdQuery { UserId = FakeIds.UserId };
           var result =  await Mediator.Send(cmd);
           Assert.NotNull(result);

        }


        [Fact]
        public async Task Should_Return_Null_When_Inactive()
        {
            var cmd = new GetUserByIdQuery { UserId = FakeIds.UserId2 };
            await Assert.ThrowsAsync<BusinessRuleException>(async () =>
            {
                await Mediator.Send(cmd);
            });
        }


        [Fact]
        public async Task Should_Return_Null_When_No_Exists()
        {
            var cmd = new GetUserByIdQuery { UserId = Guid.NewGuid() };
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await Mediator.Send(cmd);
            });
        }
    }

}
