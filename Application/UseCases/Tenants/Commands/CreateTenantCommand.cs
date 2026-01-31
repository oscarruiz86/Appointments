
using Application.Common.Tenancy;
using Application.Dtos.Tenants;
using MediatR;

namespace Application.UseCases.Tenants.Commands
{
    public record CreateTenantCommand : IRequest<CreateTenantResponseDto>, ISkipTenantValidation
    {
        public string Name { get; init; } = string.Empty;
        public string Phone { get; init; } = string.Empty;
        public string Timezone { get; init; } = string.Empty;
    }
}
