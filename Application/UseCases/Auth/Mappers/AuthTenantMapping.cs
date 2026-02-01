using Application.Dtos.Auth;
using Domain.Entities;

namespace Application.UseCases.Auth.Mappers
{
    public static class AuthTenantMapping
    {
        public static AuthTenantDto ToAuthTenantDto(this Tenant tenant)
        {
            return new AuthTenantDto
            {
                TenantId = tenant.Id,
                TenantName = tenant.Name
            };
        }
    }
}
