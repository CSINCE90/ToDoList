using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListAPI.DTO;
using ToDoListAPI.model;

namespace ToDoListAPI.service
{
    /// <summary>
    /// Espone la logica di business per consultare e gestire le attività della lista.
    /// </summary>
    public interface ITaskActivityService
    {
        Task<IEnumerable<TaskActivity>> GetAllAsync();
        Task<TaskActivity> GetByIdAsync(int id);
        Task<TaskActivity> CreateAsync(TaskActivity task);
        Task<TaskActivity> UpdateAsync(int id, UpdateTaskActivityDTO task);
        Task SoftDeleteAsync(int id);
        Task<(IEnumerable<TaskActivity> Items, int Total)> GetFilteredAsync(int? toDoListId, System.DateTime? from, System.DateTime? to, bool? isCompleted, string? q, int page, int pageSize);
    }
}

//THIS IS THE INTERFACE FOR TASKACTIVITY SERVICE 
