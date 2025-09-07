using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI.data;
using ToDoListAPI.model;

namespace ToDoListAPI.service
{
    public class ToDoListService : IToDoListService
    {
        private readonly AppDbContext _context;

        public ToDoListService(AppDbContext context)
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

        public async Task<ToDoList> CreateAsync(ToDoList list)
        {
            _context.ToDoLists.Add(list);
            await _context.SaveChangesAsync();
            return list;
        }

        public async Task<bool> UpdateAsync(int id, ToDoList list)
        {
            var existing = await _context.ToDoLists.FindAsync(id);
            if (existing == null) return false;

            existing.Name = list.Name;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.ToDoLists.FindAsync(id);
            if (existing == null) return false;

            _context.ToDoLists.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}