
using Application.Interfaces.Persistence;
using Domain.Entities;

namespace Application.Common.Rules.Tenants
{
    public class TenantPhoneUniqueRule : IRule
    {
        private readonly IUnitOfWork _uow;
        private readonly Tenant _tenant;

        public TenantPhoneUniqueRule(IUnitOfWork uow, Tenant tenant)
        {
            _uow = uow;
            _tenant = tenant;
        }

        public async Task<RuleResult> CheckAsync()
        {
            var repo = _uow.Repository<Tenant, Guid>();

            var exists = await repo.ExistsAsync(t => t.Phone == _tenant.Phone &&
                    (_tenant.Id == Guid.Empty || t.Id != _tenant.Id));
            if (exists)
                return RuleResult.Fail("Ya existe un tenant con ese teléfono");

            return RuleResult.Ok();
        }
    }
}
