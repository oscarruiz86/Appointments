using Application.Common.Rules;
using Application.Dtos.Users;
using Application.Interfaces.Infrastructure.Tenancy;
using Application.Interfaces.Persistence;
using Application.UseCases.Users.Mappers;
using Application.UseCases.Users.Queries;
using Domain.Entities.Identity;
using MediatR;

namespace Application.UseCases.Users.Handlers
{
    public class ListTenantUsersHandler : IRequestHandler<ListTenantUsersQuery, List<UserDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly RuleFactory _ruleFactory;
        private readonly ITenantContext _tenantContext;

        public ListTenantUsersHandler(
            RuleFactory ruleFactory,
            ITenantContext tenantContext,
            IUnitOfWork uow)
        {
            _uow = uow;
            _ruleFactory = ruleFactory;
            _tenantContext = tenantContext;
        }

        public async Task<List<UserDto>> Handle( ListTenantUsersQuery query, CancellationToken ct)
        {

            var tenantId = _tenantContext.TenantId!.Value;

            await RuleChecker.CheckAsync(
                   _ruleFactory.CanManageUser(tenantId)
               );

            var repo = _uow.Repository<ApplicationUser, Guid>();

            var users = repo.Query();

            if (query.OnlyActive)
                users = users.Where(x => x.IsActive);

            return users.ToDtoList();
        }

 
    }
}
