using Application.Dtos.Tenants;
using Application.UseCases.Tenants.Commands;
using Application.UseCases.Tenants.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAppointment.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TenantsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TenantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateTenantCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(Guid id) {
            var tenant =  await _mediator.Send(new GetTenantByIdQuery { Id = id });
            if (tenant is null)
                return NotFound();
            return Ok(tenant);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List([FromQuery] bool onlyActive = true)
        {
            var tenants = await _mediator.Send(new ListTenantsQuery { OnlyActive = onlyActive });
            return Ok(tenants);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, UpdateTenantDto dto)
        {
            await _mediator.Send(new UpdateTenantCommand
            {
                Id = id,
                Name = dto.Name,
                Timezone = dto.Timezone,
                Phone = dto.Phone
            });
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Disable(Guid id)
        {
            await _mediator.Send(new DisableTenantCommand {Id= id });
            return NoContent();
        }
    }
}
