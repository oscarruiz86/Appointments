
using Application.UseCases.Employees.Commands;
using Domain.Entities;

namespace Application.UseCases.Employees.Mappers
{
    public static class CreateEmployeeMapping
    {
        public static Employee ToEntity(this CreateEmployeeCommand request, Guid tenantId)
        {
            return new Employee
            {
                Id = Guid.Empty,
                TenantId = tenantId,                
                IsActive = true,
                Color = request.Color
            };
        }
    }
}
