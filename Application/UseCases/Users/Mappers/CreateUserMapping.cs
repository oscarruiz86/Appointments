using Application.UseCases.Users.Commands;
using Domain.Entities.Identity;

namespace Application.UseCases.Users.Mappers
{
    public static class CreateUserMapping
    {
        public static ApplicationUser ToEntity( this CreateUserCommand request,Guid tenantId)
        {
            var normalized = request.Email.ToUpper();

            return new ApplicationUser
            {
                Id = Guid.Empty,
                TenantId = tenantId,
                Email = request.Email,
                UserName = request.Email,
                NormalizedEmail = normalized,
                NormalizedUserName = normalized,
                FullName = request.FullName,
                IsActive = true
            };
        }
    }
}
