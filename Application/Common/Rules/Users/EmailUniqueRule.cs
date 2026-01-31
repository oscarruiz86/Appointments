
using Application.Interfaces.Persistence;
using Domain.Entities.Identity;

namespace Application.Common.Rules.Users
{
    public class EmailUniqueRule : IRule
    {
        private readonly IUnitOfWork _uow;
        private readonly ApplicationUser _user;

        public EmailUniqueRule(IUnitOfWork uow, ApplicationUser user)
        {

            _uow = uow;
            _user = user;
        }

        public async Task<RuleResult> CheckAsync()
        {
            bool exists = await _uow
                .Repository<ApplicationUser, Guid>()
                .ExistsAsync(x =>
                    x.Email == _user.Email &&
                    x.TenantId == _user.TenantId &&
                    (_user.Id == Guid.Empty || x.Id != _user.Id)
                );

            return exists
                    ? RuleResult.Fail("El email ya existe en este tenant")
                    : RuleResult.Ok();
        }
    }
}
