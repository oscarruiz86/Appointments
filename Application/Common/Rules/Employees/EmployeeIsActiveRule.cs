
using Domain.Entities;

namespace Application.Common.Rules.Employees
{
    public class EmployeeIsActiveRule : IRule
    {
        private readonly Employee _employee;

        public EmployeeIsActiveRule(Employee employee)
        {
            _employee = employee;
        }

        public Task<RuleResult> CheckAsync()
        {

            var isActive = _employee.IsActive;

            return isActive
                ? Task.FromResult(RuleResult.Ok())
                : Task.FromResult(RuleResult.Fail("Empleado inactivo"));
        }
    }

}
