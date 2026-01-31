

using Application.Tests.Common;
using Application.Tests.Common.Fake;
using Application.UseCases.Users.Commands;

namespace Application.Tests.Users
{

    public class UpdateUserHandlerTests : TestBase
    {
        [Fact]
        public async Task Should_Update_User_Data()
        {
            var cmd = new UpdateUserCommand
            {
                UserId = FakeIds.UserId,
                FullName = "Nuevo",
                Phone = "+34600123456"
            };

            await Mediator.Send(cmd);
        }
        [Fact]
        public async Task Handle_Should_Throw_When_UserDoesNotExist()
        {
            var cmd = new UpdateUserCommand
            {
                UserId = Guid.NewGuid(),
                FullName = "Nuevo",
                Phone = "+34600123456"
            };

            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await Mediator.Send(cmd);
            });
        }

    }
}
