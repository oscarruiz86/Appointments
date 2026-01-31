using Application.Interfaces.Infrastructure.Services;
using Application.Interfaces.Infrastructure.Tenancy;
using Domain.Entities.Identity;

namespace Application.Common.Rules.Users
{
    public class ChangePasswordPermissionRule : RuleBase, IRule
    {
        private readonly ApplicationUser _targetUser;

        public ChangePasswordPermissionRule(
            ICurrentUserService currentUser,
            ITenantContext tenantContext,
            ApplicationUser targetUser)
            : base(currentUser, tenantContext)
        {
            _targetUser = targetUser;
        }

        public Task<RuleResult> CheckAsync()
        {
            
            if (_currentUser.IsInRole("Admin"))
                return Task.FromResult(RuleResult.Ok());


            if (_currentUser.UserId == _targetUser.Id)
                return Task.FromResult(RuleResult.Ok());

            
            if (_currentUser.IsInRole("TenantAdmin") &&
                _tenantContext.TenantId == _targetUser.TenantId)
                return Task.FromResult(RuleResult.Ok());

            return Task.FromResult(
                RuleResult.Fail("No tiene permisos para cambiar esta contraseña"));
        }
    }

}
