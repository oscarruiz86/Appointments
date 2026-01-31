
using Application.UseCases.Users.Commands;
using Domain.Entities.Identity;

namespace Application.UseCases.Users.Mappers
{
    public static class UpdateUserMapping
    {
        public static void ToEntity(this UpdateUserCommand request, ApplicationUser user)
        {
            user.FullName = request.FullName;
            user.PhoneNumber = request.Phone;
        }
    }
}
