
using Application.Common.Tenancy;
using MediatR;

namespace Application.UseCases.Services.Commands
{
    public class DisableServiceCommand : IRequest, IRequireActiveTenant
    {
        public Guid ServiceId { get; set; }
    }
}
