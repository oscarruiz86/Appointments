using Application.Common.Tenancy;
using Application.Dtos.Employees;
using MediatR;

namespace Application.UseCases.Employees.Queries
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeDto>, IRequireActiveTenant
    {
        public Guid EmployeeId { get; set; }
    }
}

