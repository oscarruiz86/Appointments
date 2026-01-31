using Application.Common.Tenancy;
using Application.Dtos.Auth;
using MediatR;

namespace Application.UseCases.Auth.Commands
{
    public class LoginCommand : IRequest<AuthResponseDto>, ISkipTenantValidation
    {
        public  Guid TenantId { get; set; }
        public  string Email { get; set; } = string.Empty;
        public  string Password { get; set; } = string.Empty;
    }
}
