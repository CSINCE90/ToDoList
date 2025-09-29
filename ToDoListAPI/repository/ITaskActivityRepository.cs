using ToDoListAPI.model;

namespace ToDoListAPI.repository
{
    /// <summary>
    /// Definisce le operazioni di persistenza e ricerca per le attività della lista.
    /// </summary>
    public interface ITaskActivityRepository
    {
        Task<IEnumerable<TaskActivity>> GetAllAsync();
        Task<TaskActivity?> GetByIdAsync(int id);
        Task AddAsync(TaskActivity task);
        Task UpdateAsync(TaskActivity task);
        Task DeleteAsync(TaskActivity task);
        Task SaveChangesAsync();
        Task<(IEnumerable<TaskActivity> Items, int Total)> GetFilteredAsync(int? toDoListId, DateTime? from, DateTime? to, bool? isCompleted, string? q, int page, int pageSize);
    }
}

//THIS IS THE REPOSITORY INTERFACE FOR TASKACTIVITY MODEL 
