using Application.Common.Tenancy;
using Application.Dtos.Auth;
using MediatR;

namespace Application.UseCases.Auth.Commands
{
    public record ImpersonateCommand : IRequest<AuthResponseDto>, IRequireActiveTenant
    {
        public Guid TenantId { get; set; }
    }
}
