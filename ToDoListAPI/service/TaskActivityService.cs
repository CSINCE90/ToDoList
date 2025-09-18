//SERVICE   

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI.data;
using ToDoListAPI.model;
using ToDoListAPI.Exceptions;
using ToDoListAPI.repository;


namespace ToDoListAPI.service
{
    /// <summary>
    /// Service for TaskActivity
    /// </summary>
    public class TaskActivityService : ITaskActivityService
    {
        private readonly ITaskActivityRepository _tasks;
        private readonly IToDoListRepository _lists;

        public TaskActivityService(ITaskActivityRepository tasks, IToDoListRepository lists)
        {
            _tasks = tasks;
            _lists = lists;
        }

        public Task<IEnumerable<TaskActivity>> GetAllAsync() => _tasks.GetAllAsync();

        public async Task<TaskActivity> GetByIdAsync(int id)
        {
            var task = await _tasks.GetByIdAsync(id);
            if (task == null) throw new NotFoundException($"Task {id} not found.");
            return task;
        }

        public async Task<TaskActivity> CreateAsync(TaskActivity task)
        {
            if (task == null) throw new ValidationException("Task payload is required.");
            if (string.IsNullOrWhiteSpace(task.Title))
                throw new ValidationException("Task title is required.");

            // Verifica esistenza lista
            var list = await _lists.GetByIdAsync(task.ToDoListId);
            if (list == null) throw new NotFoundException($"ToDoList {task.ToDoListId} not found.");

            task.CreatedAt = DateTime.UtcNow;
            task.UpdatedAt = DateTime.UtcNow;

            await _tasks.AddAsync(task);
            await _tasks.SaveChangesAsync();
            return task;
        }

        public async Task<TaskActivity> UpdateAsync(int id, TaskActivity input)
        {
            var existing = await _tasks.GetByIdAsync(id);
            if (existing == null) throw new NotFoundException($"Task {id} not found.");
            if (input == null) throw new ValidationException("Task payload is required.");

            if (!string.IsNullOrWhiteSpace(input.Title)) existing.Title = input.Title;
            existing.Description = input.Description ?? existing.Description;
            existing.DueDate = input.DueDate;
            existing.IsCompleted = input.IsCompleted;
            existing.UpdatedAt = DateTime.UtcNow;

            await _tasks.UpdateAsync(existing);
            await _tasks.SaveChangesAsync();
            return existing;
        }

        public async Task SoftDeleteAsync(int id)
        {
            var existing = await _tasks.GetByIdAsync(id);
            if (existing == null) throw new NotFoundException($"Task {id} not found.");

            existing.IsDeleted = true;
            existing.UpdatedAt = DateTime.UtcNow;

            await _tasks.UpdateAsync(existing);
            await _tasks.SaveChangesAsync();
        }

        // Opzionale: filtri + paging già pronti in repo/interfaccia
        public async Task<(IEnumerable<TaskActivity> Items, int Total)> GetFilteredAsync(
            int? toDoListId, DateTime? from, DateTime? to, bool? isCompleted, string? q, int page, int pageSize)
        {
            if (page < 1) throw new ValidationException("page must be >= 1");
            if (pageSize < 1 || pageSize > 100) throw new ValidationException("pageSize must be between 1 and 100");
            return await _tasks.GetFilteredAsync(toDoListId, from, to, isCompleted, q, page, pageSize);
        }
    }
}