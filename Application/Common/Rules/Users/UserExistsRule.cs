
using Application.Interfaces.Persistence;
using Domain.Entities.Identity;

namespace Application.Common.Rules.Users
{
    public class UserExistsRule : IRule
    {
        private readonly IUnitOfWork _uow;
        private readonly Guid _userId;

        public UserExistsRule(IUnitOfWork uow, Guid userId)
        {
            _uow = uow;
            _userId = userId;
        }

        public async Task<RuleResult> CheckAsync()
        {
            var repo = _uow.Repository<ApplicationUser, Guid>();

            var exists = await repo.ExistsAsync(x => x.Id == _userId);

            return exists
                ? RuleResult.Ok()
                : RuleResult.Fail("Usuario no existe");
        }
    }
}
