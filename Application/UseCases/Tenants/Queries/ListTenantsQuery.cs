
using Application.Common.Tenancy;
using Application.Dtos.Tenants;
using MediatR;

namespace Application.UseCases.Tenants.Queries
{
    public record ListTenantsQuery : IRequest<List<TenantDto>>, ISkipTenantValidation
    {
        public bool OnlyActive { get; set; } = true;
    }
}
