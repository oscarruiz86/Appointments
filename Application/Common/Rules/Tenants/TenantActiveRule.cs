using Application.Interfaces.Persistence;
using Domain.Entities;
namespace Application.Common.Rules.Tenants
{
    public class TenantActiveRule : IRule
    {
        private readonly Tenant _tenant;

        public TenantActiveRule(IUnitOfWork uow, Tenant tenant)
        {
            _tenant = tenant;
        }

        public Task<RuleResult> CheckAsync()
        {

            var existsActiveTenant = _tenant.IsActive;

            if (!existsActiveTenant)
                return Task.FromResult(RuleResult.Fail("Tenant inactivo"));

            return Task.FromResult(RuleResult.Ok());
        }
    }
}
