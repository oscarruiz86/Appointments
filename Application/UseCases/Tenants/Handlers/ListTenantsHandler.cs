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
    public class ListTenantsHandler: IRequestHandler<ListTenantsQuery, List<TenantDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly AdminRule _adminRule;

        public ListTenantsHandler(IUnitOfWork uow, AdminRule adminRule)
        {
            _uow = uow;
            _adminRule = adminRule;
        }

        public async Task<List<TenantDto>> Handle(ListTenantsQuery query, CancellationToken ct)
        {

            await RuleChecker.CheckAsync(
                            _adminRule
                          );

            var q = _uow.Repository<Tenant, Guid>().Query();

            if (query.OnlyActive)
                q = q.Where(x => x.IsActive);

            var tenants = q.OrderBy(x => x.Name);

            return tenants.ToDtoList();
        }
    }
}
