
using Application.Common.Tenancy;
using Application.Dtos.Tenants;
using MediatR;

namespace Application.UseCases.Tenants.Queries
{
    public class GetTenantByIdQuery:IRequest<TenantDto>, ISkipTenantValidation
    {
        public Guid Id { get; set; }
    }
}
