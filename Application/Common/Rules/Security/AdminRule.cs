using Application.Interfaces.Infrastructure.Services;
using Application.Interfaces.Infrastructure.Tenancy;

namespace Application.Common.Rules.Security
{
    public class AdminRule : RuleBase, IStaticRule
    {
        public AdminRule(ICurrentUserService currentUser, ITenantContext tenantContext)
             : base(currentUser, tenantContext)
        {
            
        }

        public Task<RuleResult> CheckAsync()
        {
            if (!_currentUser.IsInRole("Admin"))
                return Task.FromResult(
                    RuleResult.Fail("Solo Admin"));

            return Task.FromResult(RuleResult.Ok());
        }
    }
}
