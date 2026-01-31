using Application.Common.Exceptions;
using Application.Tests.Common;
using Application.Tests.Common.Fake;
using Application.UseCases.Users.Commands;

namespace Application.Tests.Users
{
    public class ChangePasswordHandlerTests : TestBase
    {
        [Fact]
        public async Task Should_Change_Password()
        {

            var cmd = new ChangePasswordCommand
            {
                UserId = FakeIds.UserId,
                NewPassword = "P@ssw0rd!1"
            };

            await Mediator.Send(cmd);
        }

        [Fact]
        public async Task Handle_Should_Throw_When_UserDoesNotExist()
        {

            var cmd = new ChangePasswordCommand
            {
                UserId = Guid.NewGuid(),
                NewPassword = "P@ssw0rd!1"
            };

            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await Mediator.Send(cmd);
            });
        }

        [Fact]
        public async Task Handle_Should_Throw_When_PasswordIsSame()
        {

            var cmd = new ChangePasswordCommand
            {
                UserId = FakeIds.UserId,
                NewPassword = "P@ssw0rd!"
            };

            await Assert.ThrowsAsync<BusinessRuleException>(async () =>
            {
                await Mediator.Send(cmd);
            });
        }
    }

}
