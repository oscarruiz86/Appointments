using Application.Common.Tenancy;
using MediatR;

namespace Application.UseCases.Users.Commands
{
    public class DisableUserCommand : IRequest, IRequireActiveTenant
    {
        public Guid UserId { get; set; }
    }
}
