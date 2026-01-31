
using Application.Dtos.Users;
using MediatR;

namespace Application.UseCases.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public Guid UserId { get; set; }
    }
}
