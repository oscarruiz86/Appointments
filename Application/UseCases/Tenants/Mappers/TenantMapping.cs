
using Application.Dtos.Tenants;
using Domain.Entities;

namespace Application.UseCases.Tenants.Mappers
{
    public static class TenantMapping
    {
        public static TenantDto ToDto(this Tenant tenant)
        {
            return new TenantDto
            {
                Id = tenant.Id,
                Name = tenant.Name,
                Phone = tenant.Phone,
                Timezone = tenant.Timezone,
                IsActive = tenant.IsActive
            };
        }
    }
}
