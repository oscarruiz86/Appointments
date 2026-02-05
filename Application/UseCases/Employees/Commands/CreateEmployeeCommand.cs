using Application.Common.Tenancy;
using Application.Dtos.Employees;
using MediatR;

namespace Application.UseCases.Employees.Commands
{
    public class CreateEmployeeCommand : IRequest<CreateEmployeeResponseDto>, ISkipTenantValidation
    {
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
