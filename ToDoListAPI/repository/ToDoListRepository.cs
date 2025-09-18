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
    /// Repository for ToDoList
    /// </summary>
    public class ToDoListRepository : IToDoListRepository
    {

        private readonly AppDbContext _context;

        public ToDoListRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ToDoList>> GetAllAsync()
        {
            return await _context.ToDoLists
                .Include(l => l.Activities)
                .ToListAsync();
        }

        public async Task<ToDoList?> GetByIdAsync(int id)
        {
            return await _context.ToDoLists
                .Include(l => l.Activities)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task AddAsync(ToDoList list)
        {
            await _context.ToDoLists.AddAsync(list);
        }

        public Task UpdateAsync(ToDoList list)
        {
            _context.ToDoLists.Update(list);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(ToDoList list)
        {
            _context.ToDoLists.Remove(list);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<(IEnumerable<ToDoList> Items, int Total)> GetFilteredAsync(string? search, DateTime? from, DateTime? to, int page, int pageSize)
        {
            var query = _context.ToDoLists
                .Include(l => l.Activities)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(l => l.Name.Contains(search));

            if (from.HasValue)
                query = query.Where(l => l.CreatedAt >= from.Value);
            if (to.HasValue)
                query = query.Where(l => l.CreatedAt <= to.Value);

            var total = await query.CountAsync();
            var items = await query
                .OrderByDescending(l => l.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, total);
        }
    }
}

//THIS IS THE REPOSITORY IMPLEMENTATION FOR THE TO DO LIST ENTITY
