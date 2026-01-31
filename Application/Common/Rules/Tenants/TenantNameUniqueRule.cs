
using Application.Interfaces.Persistence;
using Domain.Entities;

namespace Application.Common.Rules.Tenants
{
    public class TenantNameUniqueRule : IRule
    {
        private readonly IUnitOfWork _uow;
        private readonly Tenant _tenant;

        public TenantNameUniqueRule(IUnitOfWork uow, Tenant tenant)
        {
            _uow = uow;
            _tenant = tenant;
        }

        public async Task<RuleResult> CheckAsync()
        {
            var repo = _uow.Repository<Tenant, Guid>();

            var exists = await repo.ExistsAsync(t => t.Name == _tenant.Name &&
                    (_tenant.Id == Guid.Empty || t.Id != _tenant.Id));

            if (exists)
                return RuleResult.Fail("Ya existe un tenant con ese nombre");

            return RuleResult.Ok();
        }
    }
}
