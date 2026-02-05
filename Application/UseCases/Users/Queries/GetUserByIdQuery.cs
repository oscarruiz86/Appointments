
using Application.Common.Tenancy;
using Application.Dtos.Users;
using MediatR;

namespace Application.UseCases.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserDto>, IRequireActiveTenant
    {
        public Guid UserId { get; set; }
    }
}
