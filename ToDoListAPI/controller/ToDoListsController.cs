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
        [ProducesResponseType(typeof(PagedResult<ToDoListDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<ToDoListDTO>>> GetLists([FromQuery] string? search, [FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var (items, total) = await _service.GetFilteredAsync(search, from, to, page, pageSize);
            var dtoItems = items.Select(MapToDto);
            return Ok(new PagedResult<ToDoListDTO>(dtoItems, total, page, pageSize));
        }

        // GET api/todolist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoListDTO>> GetList(int id)
        {
            var list = await _service.GetByIdAsync(id); // eccezioni gestite dal filtro globale
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

            var created = await _service.CreateAsync(list); // throw ValidationException se invalido
            var result = MapToDto(created);
            return CreatedAtAction(nameof(GetList), new { id = result.Id }, result);
        }

        // PUT api/todolist/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ToDoListDTO>> UpdateList(int id, UpdateToDoListDTO dto)
        {
            var updated = await _service.UpdateAsync(id, new ToDoList { Name = dto.Name });
            return Ok(MapToDto(updated));
        }

        /// <summary> @param id </summary> 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            await _service.DeleteAsync(id); // throw NotFound/Conflict
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
