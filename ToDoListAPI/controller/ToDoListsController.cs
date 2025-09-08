using ToDoListAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.model;
using ToDoListAPI.service;
using System.Linq;


namespace ToDoListAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoListsController : ControllerBase
    {
        private readonly IToDoListService _service;

        public ToDoListsController(IToDoListService service)
        {
            _service = service;
        }

        // GET api/todolist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoListDTO>>> GetLists()
        {
            var lists = await _service.GetAllAsync();
            var dto = lists.Select(MapToDto);
            return Ok(dto);
        }

        // GET api/todolist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoListDTO>> GetList(int id)
        {
            var list = await _service.GetByIdAsync(id);
            if (list == null) return NotFound();
            return Ok(MapToDto(list));
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

            var created = await _service.CreateAsync(list);
            var result = MapToDto(created);
            return CreatedAtAction(nameof(GetList), new { id = result.Id }, result);
        }

        // PUT api/todolist/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ToDoListDTO>> UpdateList(int id, UpdateToDoListDTO dto)
        {
            var updated = await _service.UpdateAsync(id, new ToDoList { Name = dto.Name });
            if (!updated) return NotFound();

            var list = await _service.GetByIdAsync(id);
            if (list == null) return NotFound();
            return Ok(MapToDto(list));
        }

        // DELETE api/todolist/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return Conflict("Cannot delete list with existing activities or list not found.");
            return NoContent();
        }

        private static ToDoListDTO MapToDto(ToDoList l) => new ToDoListDTO
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
        };
    }
}
