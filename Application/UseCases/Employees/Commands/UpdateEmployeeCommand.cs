using Application.Common.Tenancy;
using MediatR;

namespace Application.UseCases.Employees.Commands
{
    public class UpdateEmployeeCommand : IRequest, ISkipTenantValidation
    {
        public Guid EmployeeId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
