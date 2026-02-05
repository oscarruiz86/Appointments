
using Application.Dtos.Employees;
using Domain.Entities;

namespace Application.UseCases.Employees.Mappers
{
    public static class ListEmployeeResponseMapping
    {
        public static List<EmployeeDto> ToDtoList(this IEnumerable<Employee> employees)
        {
            return employees.Select(u => new EmployeeDto
            {
                Id = u.Id,
                Email = u.User.Email,
                FullName = u.User.FullName,
                TenantId = u.TenantId,
                Color = u.Color,
                IsActive = u.IsActive
            }).ToList();
        }
    }
}
