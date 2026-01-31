
using Application.UseCases.Tenants.Commands;
using Domain.Entities;

namespace Application.UseCases.Tenants.Mappers
{
    public static class UpdateTenantMapping
    {
        public static void MapToEntity(this UpdateTenantCommand command, Tenant tenant)
        {
            tenant.Name = command.Name;
            tenant.Phone = command.Phone;
            tenant.Timezone = command.Timezone;
        }
    }
}
