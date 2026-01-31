
using Application.Common.Tenancy;
using MediatR;

namespace Application.UseCases.Users.Commands
{
    public class ChangePasswordCommand: IRequest, IRequireActiveTenant
    {
        public  Guid UserId { get; set; }
        public string NewPassword { get; set; } = string.Empty;
    }
}
