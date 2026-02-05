
using Application.Common.Rules;
using Application.Interfaces.Persistence;
using Application.UseCases.Employees.Commands;
using Application.UseCases.Employees.Mappers;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Employees.Handlers
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly RuleFactory _ruleFactory;
        private readonly IMediator _mediator;

        public UpdateEmployeeHandler(
            RuleFactory ruleFactory,
            IMediator mediator,
            IUnitOfWork uow)
        {
            _uow = uow;
            _ruleFactory = ruleFactory;
            _mediator = mediator;
        }

        public async Task Handle(UpdateEmployeeCommand request, CancellationToken ct)
        {
            await _uow.BeginTransactionAsync();

            try
            {
                var repo = _uow.Repository<Employee, Guid>();

                var employee = await repo.GetByIdOrThrow(request.EmployeeId);
                var cmd = request.ToCommand();
                cmd.UserId = employee.UserId;
                await _mediator.Send(cmd);

                await RuleChecker.CheckAsync(
                       _ruleFactory.CanManageEmployee(employee.TenantId),
                       _ruleFactory.EmployeeIsActive(employee)
                   );

                employee.Color = request.Color;

                await repo.Update(employee);
                await _uow.SaveChangesAsync();
                await _uow.CommitAsync();
            }
            catch (Exception)
            {
                await _uow.RollbackAsync();
                throw;
            }                        
        }
    }

}
