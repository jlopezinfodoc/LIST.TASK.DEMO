using AutoMapper;
using System.Net;
using LIST.TASK.DEMO.Servicios.DTOs;
using LIST.TASK.DEMO.Servicios.Common;
using LIST.TASK.DEMO.Servicios.Interfaces;
using LIST.TASK.DEMO.Infraestructura.Repository.Interfaces;

namespace LIST.TASK.DEMO.Servicios
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<TaskDto>>> GetTasksAsync(TaskFilterDto filter)
        {
            try
            {
                var tasks = await _taskRepository.GetAllAsync(
                    filter.IsCompleted,
                    filter.Title,
                    filter.CreatedFrom,
                    filter.CreatedTo,
                    filter.PageNumber,
                    filter.PageSize);

                var taskDtos = _mapper.Map<IEnumerable<TaskDto>>(tasks);

                return ApiResponse<IEnumerable<TaskDto>>.SuccessResponse(
                    taskDtos,
                    HttpStatusCode.OK,
                    "Tareas obtenidas exitosamente");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<TaskDto>>.ErrorResponse(
                    HttpStatusCode.InternalServerError,
                    $"Error al obtener las tareas: {ex.Message}");
            }
        }

        public async Task<ApiResponse<TaskDto>> GetTaskByIdAsync(int id)
        {
            try
            {
                var task = await _taskRepository.GetByIdAsync(id);
                if (task == null)
                {
                    return ApiResponse<TaskDto>.ErrorResponse(
                        HttpStatusCode.NotFound,
                        "Tarea no encontrada");
                }

                var taskDto = _mapper.Map<TaskDto>(task);
                return ApiResponse<TaskDto>.SuccessResponse(
                    taskDto,
                    HttpStatusCode.OK,
                    "Tarea obtenida exitosamente");
            }
            catch (Exception ex)
            {
                return ApiResponse<TaskDto>.ErrorResponse(
                    HttpStatusCode.InternalServerError,
                    $"Error al obtener la tarea: {ex.Message}");
            }
        }

        public async Task<ApiResponse<TaskCreatedDto>> CreateTaskAsync(CreateTaskDto createTaskDto)
        {
            try
            {
                var task = _mapper.Map<Dominio.Task>(createTaskDto);
                var createdTask = await _taskRepository.CreateAsync(task);

                var createdDto = new TaskCreatedDto
                {
                    Id = createdTask.Id
                };

                return ApiResponse<TaskCreatedDto>.SuccessResponse(
                    createdDto,
                    HttpStatusCode.Created,
                    "Tarea creada exitosamente");
            }
            catch (Exception ex)
            {
                return ApiResponse<TaskCreatedDto>.ErrorResponse(
                    HttpStatusCode.InternalServerError,
                    $"Error al crear la tarea: {ex.Message}");
            }
        }

        public async Task<ApiResponse<TaskDto>> UpdateTaskAsync(int id, UpdateTaskDto updateTaskDto)
        {
            try
            {
                var existingTask = await _taskRepository.GetByIdAsync(id);
                if (existingTask == null)
                {
                    return ApiResponse<TaskDto>.ErrorResponse(
                        HttpStatusCode.NotFound,
                        "Tarea no encontrada");
                }

                var taskToUpdate = _mapper.Map<Dominio.Task>(updateTaskDto);
                var updatedTask = await _taskRepository.UpdateAsync(id, taskToUpdate);

                var taskDto = _mapper.Map<TaskDto>(updatedTask);
                return ApiResponse<TaskDto>.SuccessResponse(
                    taskDto,
                    HttpStatusCode.OK,
                    "Tarea actualizada exitosamente");
            }
            catch (Exception ex)
            {
                return ApiResponse<TaskDto>.ErrorResponse(
                    HttpStatusCode.InternalServerError,
                    $"Error al actualizar la tarea: {ex.Message}");
            }
        }

        public async Task<ApiResponse<TaskDto>> CompleteTaskAsync(int id)
        {
            try
            {
                var taskExists = await _taskRepository.ExistsAsync(id);
                if (!taskExists)
                {
                    return ApiResponse<TaskDto>.ErrorResponse(
                        HttpStatusCode.NotFound,
                        "Tarea no encontrada");
                }

                var success = await _taskRepository.CompleteTaskAsync(id);
                if (!success)
                {
                    return ApiResponse<TaskDto>.ErrorResponse(
                        HttpStatusCode.InternalServerError,
                        "Error al completar la tarea");
                }

                var completedTask = await _taskRepository.GetByIdAsync(id);
                var taskDto = _mapper.Map<TaskDto>(completedTask);

                return ApiResponse<TaskDto>.SuccessResponse(
                    taskDto,
                    HttpStatusCode.OK,
                    "Tarea completada exitosamente");
            }
            catch (Exception ex)
            {
                return ApiResponse<TaskDto>.ErrorResponse(
                    HttpStatusCode.InternalServerError,
                    $"Error al completar la tarea: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>> DeleteTaskAsync(int id)
        {
            try
            {
                var taskExists = await _taskRepository.ExistsAsync(id);
                if (!taskExists)
                {
                    return ApiResponse<bool>.ErrorResponse(
                        HttpStatusCode.NotFound,
                        "Tarea no encontrada");
                }

                var success = await _taskRepository.DeleteAsync(id);
                return ApiResponse<bool>.SuccessResponse(
                    success,
                    HttpStatusCode.OK,
                    "Tarea eliminada exitosamente");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse(
                    HttpStatusCode.InternalServerError,
                    $"Error al eliminar la tarea: {ex.Message}");
            }
        }
    }
}