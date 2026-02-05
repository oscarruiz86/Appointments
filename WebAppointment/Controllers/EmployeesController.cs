using Application.Dtos.Employees;
using Application.UseCases.Employees.Commands;
using Application.UseCases.Employees.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "TenantAdmin")]
        public async Task<IActionResult> Create(CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }


        [HttpGet]
        [Authorize(Roles = "TenantAdmin")]
        public async Task<IActionResult> GetEmployees([FromQuery] bool onlyActive = false)
        {
            var employees = await _mediator.Send(
                new ListEmployeeQuery { OnlyActive = onlyActive }
            );

            return Ok(employees);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "TenantAdmin")]
        public async Task<IActionResult> Update(Guid id, UpdateEmployeeDto dto)
        {
            await _mediator.Send(new UpdateEmployeeCommand
            {
                EmployeeId = id,
                FullName = dto.FullName,
                Phone = dto.Phone,
                Color = dto.Color
            });

            return NoContent();
        }


        [HttpGet("{id:guid}")]
        [Authorize(Roles = "TenantAdmin")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var employee = await _mediator.Send(new GetEmployeeByIdQuery { EmployeeId = id });

            if (employee is null)
                return NotFound();

            return Ok(employee);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "TenantAdmin")]
        public async Task<IActionResult> Disable(Guid id)
        {
            await _mediator.Send(new DisableEmployeeCommand { EmployeeId = id });

            return NoContent();
        }

    }
}
