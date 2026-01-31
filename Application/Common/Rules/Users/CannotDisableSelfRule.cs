using Application.Interfaces.Infrastructure.Services;
using Application.Interfaces.Infrastructure.Tenancy;

namespace Application.Common.Rules.Users
{
    public class CannotDisableSelfRule : RuleBase, IRule
    {
        private readonly Guid _targetUserId;

        public CannotDisableSelfRule(
            ICurrentUserService currentUser,
            ITenantContext tenantContext,
            Guid targetUserId)
            : base(currentUser, tenantContext)
        {
            _targetUserId = targetUserId;
        }

        public Task<RuleResult> CheckAsync()
        {
            if (_currentUser.UserId == _targetUserId)
                return Task.FromResult(
                    RuleResult.Fail("No puede desactivar su propio usuario"));

            return Task.FromResult(RuleResult.Ok());
        }
    }

}
