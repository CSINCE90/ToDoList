using ToDoListAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI.data;
using ToDoListAPI.model;


namespace ToDoListAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoListsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ToDoListsController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/todolist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoListDTO>>> GetLists()
        {
            var lists = await _context.ToDoLists
                .Include(l => l.Activities)
                .ToListAsync();

            var dto = lists.Select(l => new ToDoListDTO
            {
                Id = l.Id,
                Name = l.Name,
                Activities = l.Activities.Select(a => new TaskActivityDTO
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    DueDate = a.DueDate,
                    IsCompleted = a.IsCompleted,
                    ToDoListId = a.ToDoListId
                }).ToList()
            });

            return Ok(dto);
        }

        // GET api/todolist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoListDTO>> GetList(int id)
        {
            var list = await _context.ToDoLists
                .Include(l => l.Activities)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (list == null) return NotFound();

            var dto = new ToDoListDTO
            {
                Id = list.Id,
                Name = list.Name,
                Activities = list.Activities.Select(a => new TaskActivityDTO
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    DueDate = a.DueDate,
                    IsCompleted = a.IsCompleted,
                    ToDoListId = a.ToDoListId
                }).ToList()
            };

            return Ok(dto);
        }

        // POST api/todolist
        [HttpPost]
        public async Task<ActionResult<ToDoListDTO>> CreateList(CreateToDoListDTO dto)
        {
            var list = new ToDoList
            {
                Name = dto.Name,
                CreatedAt = DateTime.UtcNow
            };

            _context.ToDoLists.Add(list);
            await _context.SaveChangesAsync();

            var result = new ToDoListDTO
            {
                Id = list.Id,
                Name = list.Name,
                Activities = new List<TaskActivityDTO>()
            };

            return CreatedAtAction(nameof(GetList), new { id = list.Id }, result);
        }

        // PUT api/todolist/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ToDoListDTO>> UpdateList(int id, UpdateToDoListDTO dto)
        {
            var list = await _context.ToDoLists.Include(l => l.Activities).FirstOrDefaultAsync(l => l.Id == id);
            if (list == null) return NotFound();

            list.Name = dto.Name;
            await _context.SaveChangesAsync();

            var result = new ToDoListDTO
            {
                Id = list.Id,
                Name = list.Name,
                Activities = list.Activities.Select(a => new TaskActivityDTO
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    DueDate = a.DueDate,
                    IsCompleted = a.IsCompleted,
                    ToDoListId = a.ToDoListId
                }).ToList()
            };

            return Ok(result);
        }

        // DELETE api/todolist/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            var list = await _context.ToDoLists.FindAsync(id);
            if (list == null) return NotFound();

            _context.ToDoLists.Remove(list);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}