
using Application.Common.Rules;
using Application.Dtos.Employees;
using Application.Interfaces.Persistence;
using Application.UseCases.Employees.Mappers;
using Application.UseCases.Employees.Queries;
using Application.UseCases.Users.Queries;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Employees.Handlers
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediator _mediator;
        private readonly RuleFactory _ruleFactory;

        public GetEmployeeByIdHandler(IUnitOfWork uow, IMediator mediator, RuleFactory ruleFactory)
        {
            _uow = uow;
            _mediator = mediator;
            _ruleFactory = ruleFactory;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery query, CancellationToken ct)
        {
           var employeeRepo = _uow.Repository<Employee, Guid>();
           var employee = await employeeRepo.GetByIdOrThrow(query.EmployeeId);

           await RuleChecker.CheckAsync(
                    _ruleFactory.EmployeeIsActive(employee),
                    _ruleFactory.CanViewEmployee(employee)
                );
           
           var user = await _mediator.Send(new GetUserByIdQuery { UserId = employee.UserId });           
           return employee.ToEmployeeDto(user);
        }
    }
}
