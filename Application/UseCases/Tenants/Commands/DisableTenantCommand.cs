
using Application.Common.Tenancy;
using MediatR;

namespace Application.UseCases.Tenants.Commands
{
    public class DisableTenantCommand : IRequest, ISkipTenantValidation
    {
        public Guid Id { get; set; }
    }
}
