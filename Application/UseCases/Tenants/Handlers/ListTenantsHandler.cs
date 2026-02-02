using Application.Common.Rules;
using Application.Common.Rules.Security;
using Application.Dtos.Tenants;
using Application.Interfaces.Persistence;
using Application.UseCases.Tenants.Mappers;
using Application.UseCases.Tenants.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Tenants.Handlers
{
    public class ListTenantsHandler: IRequestHandler<ListTenantsQuery, List<TenantDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly AdminRule _adminRule;
        private readonly ILogger<ListTenantsHandler> _logger;

        public ListTenantsHandler(IUnitOfWork uow, AdminRule adminRule, ILogger<ListTenantsHandler> logger)
        {
            _uow = uow;
            _adminRule = adminRule;
            _logger = logger;
        }

        public async Task<List<TenantDto>> Handle(ListTenantsQuery query, CancellationToken ct)
        {
            _logger.LogInformation("analizas.....");
            
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
