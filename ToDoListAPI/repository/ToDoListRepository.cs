using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI.data;
using ToDoListAPI.model;

namespace ToDoListAPI.repository
{
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
    }
}

//THIS IS THE REPOSITORY IMPLEMENTATION FOR THE TO DO LIST ENTITY