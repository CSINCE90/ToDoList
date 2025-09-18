using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListAPI.model;

namespace ToDoListAPI.repository
{
    /// <summary>
    /// Interface for ToDoListRepository
    /// </summary>
    public interface IToDoListRepository
    {
        Task<IEnumerable<ToDoList>> GetAllAsync();
        Task<ToDoList?> GetByIdAsync(int id);
        Task AddAsync(ToDoList list);
        Task UpdateAsync(ToDoList list);
        Task DeleteAsync(ToDoList list);
        Task SaveChangesAsync();
        Task<(IEnumerable<ToDoList> Items, int Total)> GetFilteredAsync(string? search, DateTime? from, DateTime? to, int page, int pageSize);
    }
}

// THIS IS THE REPOSITORY INTERFACE FOR THE TO DO LIST ENTITY 
