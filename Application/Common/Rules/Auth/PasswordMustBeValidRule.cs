using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Rules.Auth
{
    public class PasswordMustBeValidRule:IRule
    {
        private readonly ApplicationUser _user;
        private readonly string _password;
        private readonly IPasswordHasher<ApplicationUser> _hasher;

        public PasswordMustBeValidRule(
             ApplicationUser user,
             string password,
             IPasswordHasher<ApplicationUser> hasher
            )
        {
            _user = user;
            _password = password;
            _hasher = hasher;
        }

        public Task<RuleResult> CheckAsync()
        {
            var result = _hasher.VerifyHashedPassword(
              _user,
              _user.PasswordHash!,
              _password);

            return result == PasswordVerificationResult.Success
               ? Task.FromResult(RuleResult.Ok())
               : Task.FromResult(RuleResult.Fail("Credenciales inválidas"));
        }
    }
}

