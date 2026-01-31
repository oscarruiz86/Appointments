using Application.Dtos.Auth;
using Domain.Entities.Identity;

namespace Application.UseCases.Auth.Mappers
{
    public static class AuthTenantMapping
    {
        public static AuthTenantDto ToAuthTenantDto(this ApplicationUser user)
        {
            return new AuthTenantDto
            {
                TenantId = user.TenantId,
                TenantName = user.Tenant!.Name
            };
        }
    }
}
