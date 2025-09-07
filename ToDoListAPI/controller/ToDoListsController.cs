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
        public async Task<ActionResult<IEnumerable<ToDoList>>> GetLists()
        {
            return await _context.ToDoLists
                .Include(l => l.Activities)
                .ToListAsync();
        }

        // GET api/todolist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoList>> GetList(int id)
        {
            var list = await _context.ToDoLists
                .Include(l => l.Activities)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (list == null) return NotFound();
            return list;
        }

        // POST api/todolist
        [HttpPost]
        public async Task<ActionResult<ToDoList>> CreateList(ToDoList list)
        {
            _context.ToDoLists.Add(list);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetList), new { id = list.Id }, list);
        }

        // PUT api/todolist/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateList(int id, ToDoList list)
        {
            if (id != list.Id) return BadRequest();

            _context.Entry(list).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
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