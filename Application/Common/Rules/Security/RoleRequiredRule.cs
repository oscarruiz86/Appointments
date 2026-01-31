using Application.Interfaces.Infrastructure.Services;

namespace Application.Common.Rules.Security
{
    public class RoleRequiredRule : IRule
    {
        private readonly ICurrentUserService _currentUser;
        private readonly string _role;

        public RoleRequiredRule(ICurrentUserService currentUser, string role)
        {
            _currentUser = currentUser;
            _role = role;
        }

        public Task<RuleResult> CheckAsync()
        {
            if (!_currentUser.IsInRole(_role))
                return Task.FromResult(
                    RuleResult.Fail($"Rol requerido: {_role}"));

            return Task.FromResult(RuleResult.Ok());
        }
    }
}
