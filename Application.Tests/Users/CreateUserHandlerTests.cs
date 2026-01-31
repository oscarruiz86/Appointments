
using Application.Common.Exceptions;
using Application.Tests.Common;
using Application.Tests.Common.Fake;
using Application.UseCases.Users.Commands;
using FluentValidation;

namespace Application.Tests.Users
{
    public class CreateUserHandlerTests: TestBase
    {

        [Fact]
        public async Task Should_Create_User()
        {
           var user = new CreateUserCommand
            {
                FullName = "Lona",
                Email = "lola@test.com",
                Phone = "+571234567890",
                Password = "Password123!",
                RoleIds = [FakeIds.AdminRoleId]
            };

            var result = await Mediator.Send(user);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Throw_When_Email_Exists()
        {
            var cmd = new CreateUserCommand
            {
                FullName = "Luis",
                Email = "luis@test.com",
                Phone = "+571234567890",
                Password = "Password123!",
                RoleIds = [FakeIds.AdminRoleId]
            };

            await Assert.ThrowsAsync<BusinessRuleException>(async () =>
            {
                await Mediator.Send(cmd);
            });
        }

        [Fact]
        public async Task Should_Throw_ValidationException_When_Email_Is_Empty()
        {
            var cmd = new CreateUserCommand
            {
                FullName = "Luis",
                Email = "", 
                Phone = "123",
                Password = "Password123!",
                RoleIds = []
            };

            await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await Mediator.Send(cmd);
            });
        }

    }
}
