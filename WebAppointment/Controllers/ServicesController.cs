using Application.Dtos.Services;
using Application.UseCases.Services.Commands;
using Application.UseCases.Services.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ServicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "TenantAdmin")]
        public async Task<IActionResult> Create(CreateServiceCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }


        [HttpGet]
        [Authorize(Roles = "TenantAdmin")]
        public async Task<IActionResult> GetServices([FromQuery] bool onlyActive = false)
        {
            var Services = await _mediator.Send(
                new ListServicesQuery { OnlyActive = onlyActive }
            );

            return Ok(Services);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "TenantAdmin")]
        public async Task<IActionResult> Update(Guid id, UpdateServiceDto dto)
        {
            await _mediator.Send(new UpdateServiceCommand
            {
                ServiceId = id,
                Name = dto.Name,
                DurationMinutes = dto.DurationMinutes,
                Price = dto.Price,
                Description = dto.Description
            });

            return NoContent();
        }


        [HttpGet("{id:guid}")]
        [Authorize(Roles = "TenantAdmin")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var Service = await _mediator.Send(new GetServiceByIdQuery { ServiceId = id });

            if (Service is null)
                return NotFound();

            return Ok(Service);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "TenantAdmin")]
        public async Task<IActionResult> Disable(Guid id)
        {
            await _mediator.Send(new DisableServiceCommand { ServiceId = id });

            return NoContent();
        }

    }
}
