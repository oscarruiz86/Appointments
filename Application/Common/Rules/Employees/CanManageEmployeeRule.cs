using Application.Interfaces.Infrastructure.Services;
using Application.Interfaces.Infrastructure.Tenancy;

namespace Application.Common.Rules.Employees
{
    public class CanManageEmployeeRule : RuleBase, IRule
    {
        private readonly Guid _targetTenant;

        public CanManageEmployeeRule(
            ICurrentUserService currentUser,
            ITenantContext tenantContext,
            Guid targetTenant)
            : base(currentUser, tenantContext)
        {
            _targetTenant = targetTenant;
        }

        public Task<RuleResult> CheckAsync()
        {
            if (_currentUser.IsInRole("Admin"))
                return Task.FromResult(RuleResult.Ok());

            if (_currentUser.IsInRole("TenantAdmin") &&
               _tenantContext.TenantId == _targetTenant)
                return Task.FromResult(RuleResult.Ok());

            return Task.FromResult(
                RuleResult.Fail("No tiene permisos para administrar este empleado"));
        }
    }

}
