

using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListAPI.model;

namespace ToDoListAPI.repository
{
    public interface ITaskActivityRepository
    {
        Task<IEnumerable<TaskActivity>> GetAllAsync();
        Task<TaskActivity?> GetByIdAsync(int id);
        Task AddAsync(TaskActivity task);
        Task UpdateAsync(TaskActivity task);
        Task DeleteAsync(TaskActivity task);
        Task SaveChangesAsync();
    }
}

//THIS IS THE REPOSITORY INTERFACE FOR TASKACTIVITY MODEL 