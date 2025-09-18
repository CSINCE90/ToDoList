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
        /// <summary>
        /// Retrieves paginated to-do lists optionally filtered by name and creation date.
        /// </summary>
        /// <param name="search">Optional search term to filter list names.</param>
        /// <param name="from">Optional lower bound for the creation date filter.</param>
        /// <param name="to">Optional upper bound for the creation date filter.</param>
        /// <param name="page">Page number starting from 1.</param>
        /// <param name="pageSize">Number of items returned per page.</param>
        /// <returns>The paginated collection of lists matching the filters.</returns>
        /// <response code="200">Returns the paginated collection of lists.</response>
        /// <response code="400">If the request parameters are invalid.</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<ToDoListDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PagedResult<ToDoListDTO>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagedResult<ToDoListDTO>>> GetLists([FromQuery] string? search, [FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var (items, total) = await _service.GetFilteredAsync(search, from, to, page, pageSize);
            var dtoItems = items.Select(MapToDto);
            return Ok(new PagedResult<ToDoListDTO>(dtoItems, total, page, pageSize));
        }

        // GET api/todolist/5
        /// <summary>
        /// Retrieves a single to-do list by its identifier.
        /// </summary>
        /// <param name="id">Identifier of the list.</param>
        /// <returns>The requested list if it exists.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoListDTO>> GetList(int id)
        {
            var list = await _service.GetByIdAsync(id); // eccezioni gestite dal filtro globale
            return Ok(MapToDto(list));
        }

        // POST api/todolist
        /// <summary>
        /// Creates a new to-do list.
        /// </summary>
        /// <param name="dto">Payload containing the list name.</param>
        /// <returns>The created list along with its identifier.</returns>
        /// <response code="201">Returns the created list.</response>
        /// <response code="400">If the request payload is invalid.</response>
        /// <response code="409">If the list name already exists.</response>
        /// <response code="500">If an unexpected error occurs.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ToDoListDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ToDoListDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ToDoListDTO), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ToDoListDTO), StatusCodes.Status500InternalServerError)]
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
        /// <summary>
        /// Updates the name of the list.
        /// </summary>
        /// <param name="id">Identifier of the list to update.</param>
        /// <param name="dto">Payload containing the new name.</param>
        /// <returns>The updated list.</returns>
        /// <response code="200">Returns the updated list.</response>
        /// <response code="400">If the request payload is invalid.</response>
        /// <response code="404">If the list is not found.</response>
        /// <response code="409">If the list name already exists.</response>
        /// <response code="500">If an unexpected error occurs.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ToDoListDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ToDoListDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ToDoListDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ToDoListDTO), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ToDoListDTO), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ToDoListDTO>> UpdateList(int id, UpdateToDoListDTO dto)
        {
            var updated = await _service.UpdateAsync(id, new ToDoList { Name = dto.Name });
            return Ok(MapToDto(updated));
        }

        // DELETE api/todolist/5
        /// <summary>
        /// Deletes the specified to-do list.
        /// </summary>
        /// <param name="id">Identifier of the list to delete.</param>
        /// <returns>No content when deletion succeeds.</returns>
        /// <response code="204">Returns no content.</response>
        /// <response code="404">If the list is not found.</response>
        /// <response code="409">If the list is not empty.</response>
        /// <response code="500">If an unexpected error occurs.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ToDoListDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ToDoListDTO), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ToDoListDTO), StatusCodes.Status500InternalServerError)]
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
