using System.Collections.Generic;

namespace ToDoListAPI.DTO
{
    /// <summary>
    /// classe generica per la paginazione
    /// </summary>
    public record PagedResult<T>(IEnumerable<T> Items, int Total, int Page, int PageSize);
}
