
using Application.Dtos.Auth;
using Domain.Entities.Identity;

namespace Application.UseCases.Auth.Mappers
{
    public static class AuthResponseMapping
    {
        public static AuthResponseDto ToAuthResponse(
            this ApplicationUser user,
            string token)
        {
            return new AuthResponseDto
            {
                Token = token,
                UserId = user.Id,
                TenantId = user.TenantId,
                Email = user.Email!
            };
        }
    }
}
