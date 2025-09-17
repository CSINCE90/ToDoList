using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListAPI.model;
using ToDoListAPI.repository;
using ToDoListAPI.Exceptions;

namespace ToDoListAPI.service
{
    public class ToDoListService : IToDoListService
    {
        private readonly IToDoListRepository _repo;

        public ToDoListService(IToDoListRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<ToDoList>> GetAllAsync() => _repo.GetAllAsync();

        public async Task<ToDoList> GetByIdAsync(int id)
        {
            var list = await _repo.GetByIdAsync(id);
            if (list == null) throw new NotFoundException($"List {id} not found.");
            return list;
        }

        public async Task<ToDoList> CreateAsync(ToDoList list)
        {
            if (list == null) throw new ValidationException("List payload is required.");
            if (string.IsNullOrWhiteSpace(list.Name))
                throw new ValidationException("List name is required.");

            await _repo.AddAsync(list);
            await _repo.SaveChangesAsync();
            return list;
        }

        public async Task<ToDoList> UpdateAsync(int id, ToDoList input)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) throw new NotFoundException($"List {id} not found.");

            if (input == null) throw new ValidationException("List payload is required.");
            if (!string.IsNullOrWhiteSpace(input.Name)) existing.Name = input.Name;

            await _repo.UpdateAsync(existing);
            await _repo.SaveChangesAsync();
            return existing;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) throw new NotFoundException($"List {id} not found.");
            // Soft delete: marca la lista come eliminata, senza rimuovere record
            existing.IsDeleted = true;
            await _repo.UpdateAsync(existing);
            await _repo.SaveChangesAsync();
        }

        public async Task<(IEnumerable<ToDoList> Items, int Total)> GetFilteredAsync(string? search, System.DateTime? from, System.DateTime? to, int page, int pageSize)
        {
            if (page < 1) throw new ValidationException("page must be >= 1");
            if (pageSize < 1 || pageSize > 100) throw new ValidationException("pageSize must be between 1 and 100");
            return await _repo.GetFilteredAsync(search, from, to, page, pageSize);
        }
    }
}
    // Service class for ToDoList: uses repository, applies business rules, throws domain exceptions.
