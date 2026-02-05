
using Application.Common.Rules;
using Application.Dtos.Employees;
using Application.Interfaces.Infrastructure.Tenancy;
using Application.Interfaces.Persistence;
using Application.UseCases.Employees.Mappers;
using Application.UseCases.Employees.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Employees.Handlers
{
    public class ListEmployeeHandler : IRequestHandler<ListEmployeeQuery, List<EmployeeDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly RuleFactory _ruleFactory;
        private readonly ITenantContext _tenantContext;

        public ListEmployeeHandler(
            RuleFactory ruleFactory,
            ITenantContext tenantContext,
            IUnitOfWork uow)
        {
            _uow = uow;
            _ruleFactory = ruleFactory;
            _tenantContext = tenantContext;
        }

        public async Task<List<EmployeeDto>> Handle(ListEmployeeQuery query, CancellationToken ct)
        {

            var tenantId = _tenantContext.TenantId!.Value;

            await RuleChecker.CheckAsync(
                   _ruleFactory.CanManageUser(tenantId)
               );

            var repo = _uow.Repository<Employee, Guid>();

            var employees = repo.Query()
                                .Include(x => x.User)
                                .Where(x => (query.OnlyActive)?x.IsActive:true);
            //.ThenInclude(us => us.UserRoles)
            //.ThenInclude(u => u.Role);


            return employees.ToDtoList();
        }


    }
}

