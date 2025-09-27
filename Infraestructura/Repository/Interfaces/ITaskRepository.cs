using LIST.TASK.DEMO.Dominio;

namespace LIST.TASK.DEMO.Infraestructura.Repository.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Dominio.Task>> GetAllAsync(
            bool? isCompleted = null,
            string? titleFilter = null,
            DateTime? createdFrom = null,
            DateTime? createdTo = null,
            int pageNumber = 1,
            int pageSize = 10);
        
        Task<Dominio.Task?> GetByIdAsync(int id);
        Task<Dominio.Task> CreateAsync(Dominio.Task task);
        Task<Dominio.Task?> UpdateAsync(int id, Dominio.Task task);
        Task<bool> CompleteTaskAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<int> GetTotalCountAsync(
            bool? isCompleted = null,
            string? titleFilter = null,
            DateTime? createdFrom = null,
            DateTime? createdTo = null);
    }
}