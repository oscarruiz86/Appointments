

using Application.Common.Rules;
using Application.Interfaces.Persistence;
using Application.UseCases.Employees.Commands;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Employees.Handlers
{
   public class DisableEmployeeHandler : IRequestHandler<DisableEmployeeCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly RuleFactory _ruleFactory;

        public DisableEmployeeHandler(
            RuleFactory ruleFactory,
            IUnitOfWork uow)
        {
            _uow = uow;
            _ruleFactory = ruleFactory;
        }

        public async Task Handle(DisableEmployeeCommand request, CancellationToken ct)
        {
            var repo = _uow.Repository<Employee, Guid>();

            var employee = await repo.GetByIdOrThrow(request.EmployeeId);

            await RuleChecker.CheckAsync(
                _ruleFactory.EmployeeMustExist(employee),
                _ruleFactory.EmployeeIsActive(employee),
                _ruleFactory.CanManageEmployee(employee.TenantId) 
            );

            employee.IsActive = false;

            await repo.Update(employee);
            await _uow.SaveChangesAsync();
        }
    }

}
