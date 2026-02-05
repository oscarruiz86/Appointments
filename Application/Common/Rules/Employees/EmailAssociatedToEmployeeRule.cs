
using Application.Dtos.Users;
using Application.Interfaces.Persistence;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Rules.Employees
{
    public class EmailAssociatedToEmployeeRule : IRule
    {
        private readonly UserDto _user;
        private readonly IUnitOfWork _uow;

        public EmailAssociatedToEmployeeRule (UserDto user, IUnitOfWork uow)
        {
            _user = user;
            _uow = uow;
        }

        public async Task<RuleResult> CheckAsync()
        {
           var rolEmployee = await _uow.Repository<ApplicationRole, Guid>().Query().FirstOrDefaultAsync(r => r.Name == "Employee");

            if (rolEmployee == null)
            {
                RuleResult.Fail("El rol 'Employee' no existe.");
            }

           return _user.Roles.Any(r => r.Id == rolEmployee.Id)
                ? RuleResult.Fail("El correo ya está asociado a un empleado.")
                : RuleResult.Ok();
        }
    }
}
