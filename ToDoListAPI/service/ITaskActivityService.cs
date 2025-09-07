using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListAPI.model;

namespace ToDoListAPI.service
{
    public interface ITaskActivityService
    {
        Task<IEnumerable<TaskActivity>> GetAllAsync();
        Task<TaskActivity?> GetByIdAsync(int id);
        Task<TaskActivity> CreateAsync(TaskActivity task);
        Task<bool> UpdateAsync(int id, TaskActivity task);
        Task<bool> SoftDeleteAsync(int id);
    }
}