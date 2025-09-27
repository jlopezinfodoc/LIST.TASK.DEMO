using LIST.TASK.DEMO.Servicios.DTOs;
using LIST.TASK.DEMO.Servicios.Common;

namespace LIST.TASK.DEMO.Servicios.Interfaces
{
    public interface ITaskService
    {
        Task<ApiResponse<IEnumerable<TaskDto>>> GetTasksAsync(TaskFilterDto filter);
        Task<ApiResponse<TaskDto>> GetTaskByIdAsync(int id);
        Task<ApiResponse<TaskCreatedDto>> CreateTaskAsync(CreateTaskDto createTaskDto);
        Task<ApiResponse<TaskDto>> UpdateTaskAsync(int id, UpdateTaskDto updateTaskDto);
        Task<ApiResponse<TaskDto>> CompleteTaskAsync(int id);
        Task<ApiResponse<bool>> DeleteTaskAsync(int id);
    }
}