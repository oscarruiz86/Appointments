
using Domain.Entities.Identity;

namespace Application.Common.Rules.Users
{
    public class UserMustExistRule : IRule
    {
        private readonly ApplicationUser _user;

        public UserMustExistRule(ApplicationUser user)
        {
            _user = user;
        }

        public Task<RuleResult> CheckAsync()
        {
            var exists = (_user != null);

            return exists
                ? Task.FromResult(RuleResult.Ok())
                : Task.FromResult(RuleResult.Fail("Usuario no existe"));
        }
    }
}
