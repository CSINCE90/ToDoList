using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListAPI.model;

namespace ToDoListAPI.service
{
    public interface ITaskActivityService
    {
        Task<IEnumerable<TaskActivity>> GetAllAsync();
        Task<TaskActivity> GetByIdAsync(int id);
        Task<TaskActivity> CreateAsync(TaskActivity task);
        Task<TaskActivity> UpdateAsync(int id, TaskActivity task);
        Task SoftDeleteAsync(int id);
        Task<(IEnumerable<TaskActivity> Items, int Total)> GetFilteredAsync(int? toDoListId, System.DateTime? from, System.DateTime? to, bool? isCompleted, string? q, int page, int pageSize);
    }
}

//THIS IS THE INTERFACE FOR TASKACTIVITY SERVICE 
