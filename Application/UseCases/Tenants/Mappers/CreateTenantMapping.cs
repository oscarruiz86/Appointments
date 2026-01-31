
using Application.UseCases.Tenants.Commands;
using Domain.Entities;

namespace Application.UseCases.Tenants.Mappers
{
    public static class CreateTenantMapping
    {
        public static Tenant ToEntity(this CreateTenantCommand command)
        {
            return new Tenant
            {
                Id = Guid.Empty,
                Name = command.Name,
                Phone = command.Phone,
                Timezone = command.Timezone,
                IsActive = true
            };
        }
    }
}
