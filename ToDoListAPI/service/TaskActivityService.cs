

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI.data;
using ToDoListAPI.model;

namespace ToDoListAPI.service
{
    public class TaskActivityService : ITaskActivityService
    {
        private readonly AppDbContext _context;

        public TaskActivityService(AppDbContext context)
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

        public async Task<TaskActivity> CreateAsync(TaskActivity task)
        {
            _context.TaskActivities.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> UpdateAsync(int id, TaskActivity task)
        {
            var existing = await _context.TaskActivities.FindAsync(id);
            if (existing == null) return false;

            existing.Title = task.Title;
            existing.Description = task.Description;
            existing.DueDate = task.DueDate;
            existing.IsCompleted = task.IsCompleted;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var existing = await _context.TaskActivities.FindAsync(id);
            if (existing == null) return false;

            existing.IsDeleted = true;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}

    // Service class for TaskActivity.
    // Handles business logic for managing TaskActivity entities,
    // including create, read, update, and soft delete operations.