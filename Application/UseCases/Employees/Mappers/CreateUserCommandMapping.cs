
using Application.UseCases.Employees.Commands;
using Application.UseCases.Users.Commands;

namespace Application.UseCases.Employees.Mappers
{
    public static class CreateUserCommandMapping
    {
        public static CreateUserCommand ToCommand(this CreateEmployeeCommand request, List<Guid> roles)
        {
            return new CreateUserCommand
            {
                FullName = request.FullName,
                Email = request.Email,
                Phone = request.Phone,
                Password = request.Password,
                RoleIds = roles
            };
        }
    }
}
