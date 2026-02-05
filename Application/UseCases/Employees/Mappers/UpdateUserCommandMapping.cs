
using Application.UseCases.Employees.Commands;
using Application.UseCases.Users.Commands;

namespace Application.UseCases.Employees.Mappers
{
    public static class UpdateUserCommandMapping
    {
        public static UpdateUserCommand ToCommand(this UpdateEmployeeCommand request)
        {
            return new UpdateUserCommand
            {
                FullName = request.FullName,
                Phone = request.Phone                
            };
        }
    }
}
