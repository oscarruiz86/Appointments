using Application.Common.Tenancy;
using MediatR;

namespace Application.UseCases.Employees.Commands
{
    public class DisableEmployeeCommand : IRequest, IRequireActiveTenant
    {
        public Guid EmployeeId { get; set; }
    }
}
