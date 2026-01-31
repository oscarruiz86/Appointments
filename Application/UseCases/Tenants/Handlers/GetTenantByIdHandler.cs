
using Application.Common.Rules;
using Application.Common.Rules.Security;
using Application.Dtos.Tenants;
using Application.Interfaces.Persistence;
using Application.UseCases.Tenants.Mappers;
using Application.UseCases.Tenants.Queries;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Tenants.Handlers
{
    public class GetTenantByIdHandler : IRequestHandler<GetTenantByIdQuery, TenantDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly AdminRule _adminRule;

        public GetTenantByIdHandler(
            IUnitOfWork uow,
            AdminRule adminRule
            )
        {
            _uow = uow;
            _adminRule = adminRule;
        }

        public async Task<TenantDto> Handle(GetTenantByIdQuery query, CancellationToken ct)
        {
            await RuleChecker.CheckAsync(
                             _adminRule
                           );

            var tenant = await _uow
                .Repository<Tenant, Guid>()
                .GetByIdOrThrow(query.Id);

            return tenant.ToDto();
        }
    }
}
