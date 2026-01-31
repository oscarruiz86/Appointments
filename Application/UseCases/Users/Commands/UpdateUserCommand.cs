
using Application.Common.Tenancy;
using MediatR;

namespace Application.UseCases.Users.Commands
{
    public class UpdateUserCommand: IRequest, IRequireActiveTenant
    {
       public Guid UserId { get; set; }
       public string FullName { get; set; } = string.Empty;
       public string Phone { get; set; } = string.Empty;
    }
}
