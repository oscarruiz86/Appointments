using Application.Interfaces.Persistence;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Rules.Users
{
    public class RolesMustExistRule : IRule
    {
        private readonly IReadOnlyCollection<Guid> _roleIds;
        private readonly IUnitOfWork _uow;

        public RolesMustExistRule(
            IUnitOfWork uow,
            IReadOnlyCollection<Guid> roleIds)
        {
            _uow = uow;
            _roleIds = roleIds;
        }

        public async Task<RuleResult> CheckAsync()
        {

            var count = await _uow
                .Repository<ApplicationRole, Guid>()
                .Query()
                .CountAsync(x =>
                    _roleIds.Contains(x.Id));
            return (count > 0 )
                ? RuleResult.Ok()
                : RuleResult.Fail("Uno o más roles no existen o no pertenecen al tenant");
        }
    }

}
