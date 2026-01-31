
using Application.Dtos.Users;
using MediatR;

namespace Application.UseCases.Users.Queries
{
    public class MeQuery : IRequest<UserDto>
    {
    }
}
