using System.Collections.Generic;

namespace ToDoListAPI.DTO
{
    /// <summary>
    /// Rappresenta un insieme di risultati paginati con metadati di conta e paginazione.
    /// </summary>
    public record PagedResult<T>(IEnumerable<T> Items, int Total, int Page, int PageSize);
}
