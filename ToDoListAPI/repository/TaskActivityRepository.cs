

using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI.data;
using ToDoListAPI.model;
using System.Linq;

namespace ToDoListAPI.repository
{
    /// <summary>
    /// Repository for TaskActivity
    /// </summary>
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

        public async Task<(IEnumerable<TaskActivity> Items, int Total)> GetFilteredAsync(int? toDoListId, DateTime? from, DateTime? to, bool? isCompleted, string? q, int page, int pageSize)
        {
            var query = _context.TaskActivities.AsQueryable();
            if (toDoListId.HasValue)
                query = query.Where(t => t.ToDoListId == toDoListId.Value);
            if (from.HasValue)
                query = query.Where(t => t.DueDate == null || t.DueDate >= from.Value);
            if (to.HasValue)
                query = query.Where(t => t.DueDate == null || t.DueDate <= to.Value);
            if (isCompleted.HasValue)
                query = query.Where(t => t.IsCompleted == isCompleted.Value);
            if (!string.IsNullOrWhiteSpace(q))
                query = query.Where(t => t.Title.Contains(q) || t.Description.Contains(q));

            var total = await query.CountAsync();
            var items = await query
                .OrderByDescending(t => t.UpdatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, total);
        }
    }
}

//THIS IS THE REPOSITORY IMPLEMENTATION FOR TASKACTIVITY MODEL 
