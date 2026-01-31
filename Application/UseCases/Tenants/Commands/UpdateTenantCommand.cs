using Application.Common.Tenancy;
using MediatR;

namespace Application.UseCases.Tenants.Commands
{
    public record UpdateTenantCommand : IRequest, ISkipTenantValidation
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Timezone { get; set; } = string.Empty;
    }
}
