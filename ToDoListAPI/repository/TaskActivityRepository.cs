

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI.data;
using ToDoListAPI.model;

namespace ToDoListAPI.repository
{
    public class TaskActivityRepository : ITaskActivityRepository
    {
        private readonly AppDbContext _context;

        public TaskActivityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskActivity>> GetAllAsync()
        {
            return await _context.TaskActivities.ToListAsync();
        }

        public async Task<TaskActivity?> GetByIdAsync(int id)
        {
            return await _context.TaskActivities.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(TaskActivity task)
        {
            await _context.TaskActivities.AddAsync(task);
        }

        public Task UpdateAsync(TaskActivity task)
        {
            _context.TaskActivities.Update(task);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TaskActivity task)
        {
            _context.TaskActivities.Remove(task);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

//THIS IS THE REPOSITORY IMPLEMENTATION FOR TASKACTIVITY MODEL 