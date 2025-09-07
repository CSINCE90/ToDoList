using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListAPI.model;

namespace ToDoListAPI.service
{
    public interface IToDoListService
    {
        Task<IEnumerable<ToDoList>> GetAllAsync();
        Task<ToDoList?> GetByIdAsync(int id);
        Task<ToDoList> CreateAsync(ToDoList list);
        Task<bool> UpdateAsync(int id, ToDoList list);
        Task<bool> DeleteAsync(int id);
    }
}