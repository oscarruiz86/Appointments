
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Rules.Users
{
    public class PasswordNotSameRule : IRule
    {
        private readonly ApplicationUser _user;
        private readonly string _newPassword;
        private readonly IPasswordHasher<ApplicationUser> _hasher;

        public PasswordNotSameRule(
            ApplicationUser user,
            string newPassword,
            IPasswordHasher<ApplicationUser> hasher)
        {
            _user = user;
            _newPassword = newPassword;
            _hasher = hasher;
        }

        public Task<RuleResult> CheckAsync()
        {
            var result = _hasher.VerifyHashedPassword(
                _user,
                _user.PasswordHash!,
                _newPassword);

            if (result != PasswordVerificationResult.Failed)
                return Task.FromResult(
                    RuleResult.Fail("La nueva contraseña no puede ser igual a la actual"));

            return Task.FromResult(RuleResult.Ok());
        }
    }

}
