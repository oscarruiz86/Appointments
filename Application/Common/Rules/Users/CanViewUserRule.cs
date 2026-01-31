using Application.Interfaces.Infrastructure.Services;
using Application.Interfaces.Infrastructure.Tenancy;
using Domain.Entities.Identity;

namespace Application.Common.Rules.Users
{
    public class CanViewUserRule : RuleBase, IRule
    {
        private readonly ApplicationUser _target;

        public CanViewUserRule(
            ICurrentUserService currentUser,
            ITenantContext tenantContext,
            ApplicationUser target)
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
                RuleResult.Fail("No tiene permisos para consultar este usuario"));
        }
    }
}
