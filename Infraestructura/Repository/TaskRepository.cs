using Microsoft.EntityFrameworkCore;
using LIST.TASK.DEMO.Infraestructura.Persistencia;
using LIST.TASK.DEMO.Infraestructura.Repository.Interfaces;

namespace LIST.TASK.DEMO.Infraestructura.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;

        public TaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dominio.Task>> GetAllAsync(
            bool? isCompleted = null,
            string? titleFilter = null,
            DateTime? createdFrom = null,
            DateTime? createdTo = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _context.Tasks.AsQueryable();

            if (isCompleted.HasValue)
            {
                query = query.Where(t => t.IsCompleted == isCompleted.Value);
            }

            if (!string.IsNullOrWhiteSpace(titleFilter))
            {
                query = query.Where(t => t.Title.Contains(titleFilter));
            }

            if (createdFrom.HasValue)
            {
                query = query.Where(t => t.CreatedDate >= createdFrom.Value);
            }

            if (createdTo.HasValue)
            {
                query = query.Where(t => t.CreatedDate <= createdTo.Value);
            }

            return await query
                .OrderByDescending(t => t.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Dominio.Task?> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<Dominio.Task> CreateAsync(Dominio.Task task)
        {
            task.CreatedDate = DateTime.Now;
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Dominio.Task?> UpdateAsync(int id, Dominio.Task task)
        {
            var existingTask = await _context.Tasks.FindAsync(id);
            if (existingTask == null)
                return null;

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.IsCompleted = task.IsCompleted;
            
            if (task.IsCompleted && !existingTask.IsCompleted)
            {
                existingTask.CompletedDate = DateTime.Now;
            }
            else if (!task.IsCompleted)
            {
                existingTask.CompletedDate = null;
            }

            await _context.SaveChangesAsync();
            return existingTask;
        }

        public async Task<bool> CompleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return false;

            task.IsCompleted = true;
            task.CompletedDate = DateTime.Now;
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Tasks.AnyAsync(t => t.Id == id);
        }

        public async Task<int> GetTotalCountAsync(
            bool? isCompleted = null,
            string? titleFilter = null,
            DateTime? createdFrom = null,
            DateTime? createdTo = null)
        {
            var query = _context.Tasks.AsQueryable();

            if (isCompleted.HasValue)
            {
                query = query.Where(t => t.IsCompleted == isCompleted.Value);
            }

            if (!string.IsNullOrWhiteSpace(titleFilter))
            {
                query = query.Where(t => t.Title.Contains(titleFilter));
            }

            if (createdFrom.HasValue)
            {
                query = query.Where(t => t.CreatedDate >= createdFrom.Value);
            }

            if (createdTo.HasValue)
            {
                query = query.Where(t => t.CreatedDate <= createdTo.Value);
            }

            return await query.CountAsync();
        }
    }
}