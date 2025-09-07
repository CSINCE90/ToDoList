using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListAPI.model;

namespace ToDoListAPI.repository
{
    public interface IToDoListRepository
    {
        Task<IEnumerable<ToDoList>> GetAllAsync();
        Task<ToDoList?> GetByIdAsync(int id);
        Task AddAsync(ToDoList list);
        Task UpdateAsync(ToDoList list);
        Task DeleteAsync(ToDoList list);
        Task SaveChangesAsync();
    }
}