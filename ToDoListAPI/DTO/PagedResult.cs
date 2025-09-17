using System.Collections.Generic;

namespace ToDoListAPI.DTO
{
    public record PagedResult<T>(IEnumerable<T> Items, int Total, int Page, int PageSize);
}

