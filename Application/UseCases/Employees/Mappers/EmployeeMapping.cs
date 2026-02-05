
using Application.Dtos.Employees;
using Application.Dtos.Users;
using Domain.Entities;

namespace Application.UseCases.Employees.Mappers
{
    public static class EmployeeMapping
    {
        public static EmployeeDto ToEmployeeDto(this Employee? employee, UserDto user)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                IsActive = employee.IsActive,
                Color = employee.Color,                
                Email = user.Email!,
                FullName = user.FullName,
                TenantId = user.TenantId
            };
        }
    }
}
