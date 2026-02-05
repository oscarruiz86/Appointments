
using Application.Common.Tenancy;
using Application.Dtos.Users;
using MediatR;

namespace Application.UseCases.Users.Queries
{
    public class GetByUserEmailQuery : IRequest<UserDto?>, IRequireActiveTenant
    {
        public string Email { get; set; } = string.Empty;
    }
}
