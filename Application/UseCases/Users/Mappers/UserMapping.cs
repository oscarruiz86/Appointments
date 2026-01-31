using Application.Dtos.Roles;
using Application.Dtos.Users;
using Domain.Entities.Identity;


namespace Application.UseCases.Users.Mappers
{
    public static class UserMapping
    {
        public static UserDto ToUser(this ApplicationUser user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email!,
                FullName = user.FullName,
                TenantId = user.TenantId,
                Roles = user.UserRoles?
                    .Where(ur => ur.Role != null)
                    .Select(ur => new RoleDto
                    {
                        Id = ur.Role.Id,
                        Name = ur.Role.Name!
                    })
                    .ToList()
                    ?? new List<RoleDto>()
            };
        }
    }
}
