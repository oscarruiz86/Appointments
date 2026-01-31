using Domain.Entities.Identity;

namespace Application.Common.Rules.Users
{
    public class UserIsActiveRule : IRule
    {
        private readonly ApplicationUser _user;

        public UserIsActiveRule(ApplicationUser user)
        {
            _user = user;
        }

        public Task<RuleResult> CheckAsync()
        {

            var isActive = _user.IsActive;

            return isActive
                ? Task.FromResult(RuleResult.Ok())
                : Task.FromResult(RuleResult.Fail("Usuario inactivo")) ;
        }
    }

}
