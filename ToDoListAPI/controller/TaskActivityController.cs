using ToDoListAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI.data;
using ToDoListAPI.model;

namespace ToDoListAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskActivityController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskActivityController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/taskactivity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskActivity>>> GetTasks()
        {
            return await _context.TaskActivities.ToListAsync();
        }

        // GET api/taskactivity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskActivity>> GetTask(int id)
        {
            var task = await _context.TaskActivities.FindAsync(id);
            if (task == null) return NotFound();
            return task;
        }

        // POST api/taskactivity
        [HttpPost]
        public async Task<ActionResult<TaskActivity>> CreateTask(CreateTaskActivityDTO dto)
        {
            var task = new TaskActivity
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                ToDoListId = dto.ToDoListId,
                CreatedAt = DateTime.UtcNow
            };

            _context.TaskActivities.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        // PUT api/taskactivity/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskActivityDTO dto)
        {
            var task = await _context.TaskActivities.FindAsync(id);
            if (task == null) return NotFound();

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.DueDate = dto.DueDate;
            task.IsCompleted = dto.IsCompleted;
            task.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/taskactivity/5 (soft delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.TaskActivities.FindAsync(id);
            if (task == null) return NotFound();

            task.IsDeleted = true;
            task.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}