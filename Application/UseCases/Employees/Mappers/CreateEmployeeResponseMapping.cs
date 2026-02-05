
using Application.Dtos.Employees;
using Domain.Entities;

namespace Application.UseCases.Employees.Mappers
{
    public static class CreateEmployeeResponseMapping
    {
        public static CreateEmployeeResponseDto ToCreateResponse(this Employee employee)
        {
            return new CreateEmployeeResponseDto
            {
                Id = employee.Id
            };
        }
    }
}
