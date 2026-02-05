using Application.Common.Tenancy;
using Application.Dtos.Employees;
using MediatR;

namespace Application.UseCases.Employees.Queries
{
    public class ListEmployeeQuery : IRequest<List<EmployeeDto>>, IRequireActiveTenant
    {
        public bool OnlyActive { get; set; } = true;
    }
}