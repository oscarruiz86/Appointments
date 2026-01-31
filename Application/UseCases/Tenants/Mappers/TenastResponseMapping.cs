
using Application.Dtos.Tenants;
using Domain.Entities;

namespace Application.UseCases.Tenants.Mappers
{
    public static class TenastResponseMapping
    {
        public static CreateTenantResponseDto ToCreateResponse(this Tenant tenat)
        {
            return new CreateTenantResponseDto
            {
                Id = tenat.Id
            };
        }
    }
}
