using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Common.Dtos;
using TaskManagement.Application.Features.TaskItem.Commands.CreateTask;
using TaskManagement.Application.Features.TaskItem.Commands.DeleteTask;
using TaskManagement.Application.Features.TaskItem.Queries.GetAllTask;
using TaskManagement.Application.Features.TaskItem.Queries.GetByIdTask;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskItemController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskCreateDto request, CancellationToken ct = default)
        {
            var command = new CreateTaskCommand(request);

            var result = await _mediator.Send(command, ct);
            return Ok(result);

        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteTask(Guid id, CancellationToken ct = default)
        {
            await _mediator.Send(new DeleteTaskCommand(id), ct);
            return Ok("Deleted successfully");
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetTaskById(Guid id, CancellationToken ct = default)
        {
            var result = await _mediator.Send(new GetByIdTaskQuery(id), ct);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTask([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
        {
            var result = await _mediator.Send(new GetAllTaskQuery(pageNumber, pageSize), ct);
            return Ok(result);
        }
    }
}
