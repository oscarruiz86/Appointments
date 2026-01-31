
using Application.Common.Exceptions;
using Application.Tests.Common;
using Application.UseCases.Tenants.Commands;

namespace Application.Tests.Tenants
{
    public class CreateTenantHandlerTests : TestBase
    {
      
        [Fact]
        public async Task Should_Create_Tenant()
        {
           var cmd = new CreateTenantCommand
           {
               Name = "Salon A",
               Phone = "+34600123456",
               Timezone = "UTC"
           };
            await Mediator.Send(cmd);
        }

        [Fact]
        public async Task Should_Throw_When_Name_Exists()
        {
            var cmd = new CreateTenantCommand
            {
                Name = "Test Tenant",
                Phone = "+34600123456",
                Timezone = "UTC"
            };

            await Assert.ThrowsAsync<BusinessRuleException>(async () =>
            {
                await Mediator.Send(cmd);
            });
        }
    }
}
