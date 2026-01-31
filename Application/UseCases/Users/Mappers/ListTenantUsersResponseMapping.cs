

using Application.Dtos.Users;
using Domain.Entities.Identity;

namespace Application.UseCases.Users.Mappers
{
    public static class ListTenantUsersResponseMapping
    {
        public static List<UserDto> ToDtoList(this IEnumerable<ApplicationUser> users)
        {
            return users.Select(u => u.ToUser()).ToList();
        }
    }
}
