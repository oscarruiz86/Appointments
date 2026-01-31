
using Domain.Entities.Identity;

namespace Application.Common.Rules.Users
{
    public class UserMustHaveTenantRule : IRule
    {

        private readonly ApplicationUser _user;

        public UserMustHaveTenantRule(ApplicationUser user)
        {
            _user = user;
        }

        public Task<RuleResult> CheckAsync()
        {
            var isValid = ( _user.Tenant != null);

            return isValid
               ? Task.FromResult(RuleResult.Ok())
               : Task.FromResult(RuleResult.Fail("Usuario sin configuración de Tenant"));
            }
        }
}
