using Application.Common.Tenancy;
using Application.Dtos.Users;
using MediatR;

namespace Application.UseCases.Users.Commands
{
    public class CreateUserCommand : IRequest<CreateUserResponseDto>, IRequireActiveTenant
    {
        public  string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public List<Guid> RoleIds { get; init; } = [];
    }
}
