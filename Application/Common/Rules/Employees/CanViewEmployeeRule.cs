
using Application.Interfaces.Infrastructure.Services;
using Application.Interfaces.Infrastructure.Tenancy;
using Domain.Entities;

namespace Application.Common.Rules.Employees
{
    public class CanViewEmployeeRule : RuleBase, IRule
    {
        private readonly Employee _target;

        public CanViewEmployeeRule(
            ICurrentUserService currentUser,
            ITenantContext tenantContext,
            Employee target)
            : base(currentUser, tenantContext)
        {
            _target = target;
        }

        public Task<RuleResult> CheckAsync()
        {

            if (_currentUser.IsInRole("Admin"))
                return Task.FromResult(RuleResult.Ok());

            if (_currentUser.UserId == _target.Id)
                return Task.FromResult(RuleResult.Ok());

            if (_currentUser.IsInRole("TenantAdmin") &&
                _tenantContext.TenantId == _target.TenantId)
                return Task.FromResult(RuleResult.Ok());

            return Task.FromResult(
                RuleResult.Fail("No tiene permisos para consultar este empleado"));
        }
    }
}
