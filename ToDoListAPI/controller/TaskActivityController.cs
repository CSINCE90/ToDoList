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
        /// <summary>
        /// Retrieves paginated to-do lists optionally filtered by name and creation date.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<TaskActivityDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PagedResult<TaskActivityDTO>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PagedResult<TaskActivityDTO>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(PagedResult<TaskActivityDTO>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedResult<TaskActivityDTO>>> GetTasks([FromQuery] int? toDoListId, [FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] bool? isCompleted, [FromQuery] string? description, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var (items, total) = await _service.GetFilteredAsync(toDoListId, from, to, isCompleted, description, page, pageSize);
            var dto = items.Select(MapToDto);
            return Ok(new PagedResult<TaskActivityDTO>(dto, total, page, pageSize));
        }

        // GET api/taskactivity/5
        /// <summary>
        /// Retrieves a single to-do list by its identifier.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaskActivityDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TaskActivityDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TaskActivityDTO), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskActivityDTO>> GetTask(int id)
        {
            var task = await _service.GetByIdAsync(id); // eccezioni gestite dal filtro globale
            return Ok(MapToDto(task));
        }

        // POST api/taskactivity
        /// <summary>
        /// Creates a new to-do list.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(TaskActivityDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(TaskActivityDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(TaskActivityDTO), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(TaskActivityDTO), StatusCodes.Status500InternalServerError)]
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
        /// <summary>
        /// Updates the name of the list.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TaskActivityDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(TaskActivityDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(TaskActivityDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TaskActivityDTO), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(TaskActivityDTO), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskActivityDTO dto)
        {
            await _service.UpdateAsync(id, dto); // throw NotFound/Validation
            return NoContent();
        }

        // DELETE api/taskactivity/5 (soft delete)
        /// <summary>
        /// Deletes the specified to-do list.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(TaskActivityDTO), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(TaskActivityDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TaskActivityDTO), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _service.SoftDeleteAsync(id); // throw NotFound
            return NoContent();
        }

        /// <summary>
        /// Maps a TaskActivity to a TaskActivityDTO
        /// </summary>
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
