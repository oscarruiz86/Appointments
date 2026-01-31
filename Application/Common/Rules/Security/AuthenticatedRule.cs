using Application.Interfaces.Infrastructure.Services;

namespace Application.Common.Rules.Security
{
    public class AuthenticatedRule : IStaticRule
    {
        private readonly ICurrentUserService _currentUser;

        public AuthenticatedRule(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }

        public Task<RuleResult> CheckAsync()
        {
            if (_currentUser.UserId == Guid.Empty)
                return Task.FromResult(
                    RuleResult.Fail("Usuario no autenticado"));

            return Task.FromResult(RuleResult.Ok());
        }
    }
}
