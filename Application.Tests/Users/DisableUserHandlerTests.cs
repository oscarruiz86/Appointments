
using Application.Common.Exceptions;
using Application.Tests.Common;
using Application.Tests.Common.Fake;
using Application.UseCases.Users.Commands;


namespace Application.Tests.Users
{
 
    public class DisableUserHandlerTests : TestBase
    {
        [Fact]
        public async Task Should_Disable_User()
        {
            var cmd = new DisableUserCommand { UserId = FakeIds.UserId3 };
            await Mediator.Send(cmd);

        }

        [Fact]
        public async Task Handle_Should_Throw_When_UserDoesNotExist()
        {
            var cmd = new DisableUserCommand { UserId =  Guid.NewGuid()};
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await Mediator.Send(cmd);
            });
        }

        [Fact]
        public async Task Handle_Should_Throw_When_DisableSelf()
        {
            var cmd = new DisableUserCommand { UserId = FakeIds.UserId };
            await Assert.ThrowsAsync<BusinessRuleException>(async () =>
            {
                await Mediator.Send(cmd);
            });
        }

    }
 }
