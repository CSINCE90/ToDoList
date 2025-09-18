using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListAPI.model;

namespace ToDoListAPI.service
{
    /// <summary>
    /// Interface for ToDoListService
    /// </summary>
    public interface IToDoListService
    {
        Task<IEnumerable<ToDoList>> GetAllAsync();
        Task<ToDoList> GetByIdAsync(int id);
        Task<ToDoList> CreateAsync(ToDoList list);
        Task<ToDoList> UpdateAsync(int id, ToDoList list);
        Task DeleteAsync(int id);
        Task<(IEnumerable<ToDoList> Items, int Total)> GetFilteredAsync(string? search, DateTime? from, DateTime? to, int page, int pageSize);
    }
}

//THIS IS THE INTERFACE FOR THE TO DO LIST SERVICE
