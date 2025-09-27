using Microsoft.AspNetCore.Mvc;
using LIST.TASK.DEMO.Servicios.Interfaces;
using LIST.TASK.DEMO.Servicios.DTOs;
using System.Net;

namespace LIST.TASK.DEMO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Obtiene todas las tareas con filtros opcionales
        /// </summary>
        /// <param name="isCompleted">Filtrar por estado de completado</param>
        /// <param name="title">Filtrar por título</param>
        /// <param name="createdFrom">Filtrar desde fecha de creación</param>
        /// <param name="createdTo">Filtrar hasta fecha de creación</param>
        /// <param name="pageNumber">Número de página</param>
        /// <param name="pageSize">Tamaño de página</param>
        /// <returns>Lista de tareas</returns>
        [HttpGet]
        public async Task<IActionResult> GetTasks(
            [FromQuery] bool? isCompleted = null,
            [FromQuery] string? title = null,
            [FromQuery] DateTime? createdFrom = null,
            [FromQuery] DateTime? createdTo = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var filter = new TaskFilterDto
            {
                IsCompleted = isCompleted,
                Title = title,
                CreatedFrom = createdFrom,
                CreatedTo = createdTo,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var response = await _taskService.GetTasksAsync(filter);
            
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Obtiene una tarea por su ID
        /// </summary>
        /// <param name="id">ID de la tarea</param>
        /// <returns>Tarea encontrada</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var response = await _taskService.GetTaskByIdAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Crea una nueva tarea
        /// </summary>
        /// <param name="createTaskDto">Datos de la tarea a crear</param>
        /// <returns>ID de la tarea creada</returns>
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto createTaskDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Datos de entrada inválidos",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });
            }

            var response = await _taskService.CreateTaskAsync(createTaskDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Actualiza completamente una tarea
        /// </summary>
        /// <param name="id">ID de la tarea</param>
        /// <param name="updateTaskDto">Nuevos datos de la tarea</param>
        /// <returns>Tarea actualizada</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskDto updateTaskDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Datos de entrada inválidos",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });
            }

            var response = await _taskService.UpdateTaskAsync(id, updateTaskDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Marca una tarea como completada
        /// </summary>
        /// <param name="id">ID de la tarea</param>
        /// <returns>Tarea completada</returns>
        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> CompleteTask(int id)
        {
            var response = await _taskService.CompleteTaskAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Elimina una tarea
        /// </summary>
        /// <param name="id">ID de la tarea</param>
        /// <returns>Confirmación de eliminación</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var response = await _taskService.DeleteTaskAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}