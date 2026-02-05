using Domain.Entities;

namespace Application.Common.Rules.Employees
{
    public class EmployeeMustExistRule : IRule
    {
        private readonly Employee _employee;

        public EmployeeMustExistRule(Employee employee)
        {
            _employee = employee;
        }

        public Task<RuleResult> CheckAsync()
        {
            var exists = (_employee != null);

            return exists
                ? Task.FromResult(RuleResult.Ok())
                : Task.FromResult(RuleResult.Fail("Empleado no existe"));
        }
    }
}
