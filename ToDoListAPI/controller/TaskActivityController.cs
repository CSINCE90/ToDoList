using ToDoListAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.model;
using ToDoListAPI.service;
using System.Linq;

namespace ToDoListAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskActivityController : ControllerBase
    {
        private readonly ITaskActivityService _service;

        public TaskActivityController(ITaskActivityService service)
        {
            _service = service;
        }

        // GET api/taskactivity
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<TaskActivityDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<TaskActivityDTO>>> GetTasks([FromQuery] int? toDoListId, [FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] bool? isCompleted, [FromQuery] string? q, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var (items, total) = await _service.GetFilteredAsync(toDoListId, from, to, isCompleted, q, page, pageSize);
            var dto = items.Select(MapToDto);
            return Ok(new PagedResult<TaskActivityDTO>(dto, total, page, pageSize));
        }

        // GET api/taskactivity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskActivityDTO>> GetTask(int id)
        {
            var task = await _service.GetByIdAsync(id); // throw NotFound
            return Ok(MapToDto(task));
        }

        // POST api/taskactivity
        [HttpPost]
        public async Task<ActionResult<TaskActivityDTO>> CreateTask(CreateTaskActivityDTO dto)
        {
            var task = new TaskActivity
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                ToDoListId = dto.ToDoListId,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _service.CreateAsync(task); // throws Validation/NotFound
            var result = MapToDto(created);
            return CreatedAtAction(nameof(GetTask), new { id = result.Id }, result);
        }

        // PUT api/taskactivity/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskActivityDTO dto)
        {
            var task = new TaskActivity
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                IsCompleted = dto.IsCompleted
            };

            await _service.UpdateAsync(id, task); // throw NotFound/Validation
            return NoContent();
        }

        // DELETE api/taskactivity/5 (soft delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _service.SoftDeleteAsync(id); // throw NotFound
            return NoContent();
        }

        private static TaskActivityDTO MapToDto(TaskActivity a) => new TaskActivityDTO
        {
            Id = a.Id,
            Title = a.Title,
            Description = a.Description,
            DueDate = a.DueDate,
            IsCompleted = a.IsCompleted,
            ToDoListId = a.ToDoListId
        };
    }
}
