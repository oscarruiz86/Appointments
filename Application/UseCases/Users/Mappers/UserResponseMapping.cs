
using Application.Dtos.Users;
using Domain.Entities.Identity;

namespace Application.UseCases.Users.Mappers
{
    public static class CreateUserResponseMapping
    {
        public static CreateUserResponseDto ToCreateResponse(this ApplicationUser user)
        {
            return new CreateUserResponseDto
            {
                Id = user.Id
            };
        }
    }
}
