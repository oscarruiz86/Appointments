using Application.Interfaces.Infrastructure.Services;
using Application.Interfaces.Infrastructure.Tenancy;

namespace Application.Common.Rules
{
    public abstract class RuleBase
    {
        protected readonly ICurrentUserService _currentUser;
        protected readonly ITenantContext _tenantContext;
        

        protected RuleBase(ICurrentUserService currentUser, ITenantContext tenantContext)
        {
            _currentUser = currentUser;
            _tenantContext = tenantContext;
        }
    }
}
