using Application.Common.Rules;
using Application.Dtos.Employees;
using Application.Interfaces.Infrastructure.Tenancy;
using Application.Interfaces.Persistence;
using Application.Interfaces.Persistence.Filters;
using Application.UseCases.Employees.Commands;
using Application.UseCases.Employees.Mappers;
using Application.UseCases.Users.Queries;
using Domain.Entities;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Employees.Handlers
{
    public class CreateEmployeeHandler: IRequestHandler<CreateEmployeeCommand, CreateEmployeeResponseDto>
    {
        private const string EmployeeRole = "Employee";
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _uow;
        private readonly IFilterContext _filterContext;
        private readonly ITenantContext _tenantContext;
        private readonly RuleFactory _ruleFactory;

        public CreateEmployeeHandler(
            IMediator mediator, 
            IUnitOfWork uow, 
            IFilterContext filterContext, 
            ITenantContext tenantContext,
            RuleFactory ruleFactory
            )
        {
            _mediator = mediator;
            _uow = uow;
            _filterContext = filterContext;
            _tenantContext = tenantContext;
            _ruleFactory = ruleFactory;
        }

        public async Task<CreateEmployeeResponseDto> Handle(
            CreateEmployeeCommand request,
            CancellationToken cancellationToken)
        {
            await _uow.BeginTransactionAsync();
            try
            {

                var employeeRepo = _uow.Repository<Employee, Guid>();
                var roleRepo = _uow.Repository<ApplicationRole, Guid>();
                Guid tenantId = _tenantContext.TenantId.Value;
                var user = await _mediator.Send(new GetByUserEmailQuery { Email = request.Email });

                if (user == null)
                {
                    var role = await roleRepo.Query().FirstOrDefaultAsync(r => r.Name == EmployeeRole);
                    var userCmd = request.ToCommand(new List<Guid> { role.Id });
                    var usercmd = await _mediator.Send(userCmd, cancellationToken);
                    user = await _mediator.Send(new GetByUserEmailQuery { Email = request.Email });
                }
                else
                {
                    await RuleChecker.CheckAsync(
                        _ruleFactory.EmailAssociatedToEmployee(user)
                    );
                }

                var employee = request.ToEntity(tenantId);
                employee.UserId = user.Id;
                await employeeRepo.Add(employee);
                await _uow.SaveChangesAsync();
                await _uow.CommitAsync();
                return employee.ToCreateResponse();
            }
            catch
            {
                await _uow.RollbackAsync();
                throw;
            }

           
        }
    }
}
