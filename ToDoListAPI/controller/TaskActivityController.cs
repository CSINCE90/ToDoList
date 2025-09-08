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
        public async Task<ActionResult<IEnumerable<TaskActivityDTO>>> GetTasks()
        {
            var tasks = await _service.GetAllAsync();
            var dto = tasks.Select(MapToDto);
            return Ok(dto);
        }

        // GET api/taskactivity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskActivityDTO>> GetTask(int id)
        {
            var task = await _service.GetByIdAsync(id);
            if (task == null) return NotFound();
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

            var created = await _service.CreateAsync(task);
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

            var updated = await _service.UpdateAsync(id, task);
            if (!updated) return NotFound();
            return NoContent();
        }

        // DELETE api/taskactivity/5 (soft delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var deleted = await _service.SoftDeleteAsync(id);
            if (!deleted) return NotFound();
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
