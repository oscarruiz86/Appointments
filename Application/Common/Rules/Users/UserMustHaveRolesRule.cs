
using Domain.Entities.Identity;

namespace Application.Common.Rules.Users
{
    public class UserMustHaveRolesRule: IRule
    {

        private readonly ApplicationUser _user;

        public UserMustHaveRolesRule(ApplicationUser user)
        {
            _user = user;
        }

        public Task<RuleResult> CheckAsync()
        {
            var isiNValid = (_user.UserRoles == null || _user.UserRoles.Count == 0);

            return isiNValid
               ? Task.FromResult(RuleResult.Fail("Usuario sin configuración de Rol"))
               : Task.FromResult(RuleResult.Ok());
            }
        }
}
