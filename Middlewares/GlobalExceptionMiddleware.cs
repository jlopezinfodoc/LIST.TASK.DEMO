using System.Net;
using System.Text.Json;
using LIST.TASK.DEMO.Servicios.Common;
using LIST.TASK.DEMO.Servicios.Exceptions;

namespace LIST.TASK.DEMO.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = exception switch
            {
                // Excepciones personalizadas para Task
                TaskNotFoundException => ApiResponse<object>.ErrorResponse(
                    HttpStatusCode.NotFound,
                    exception.Message),
                
                TaskValidationException => ApiResponse<object>.ErrorResponse(
                    HttpStatusCode.BadRequest,
                    exception.Message),
                
                TaskAlreadyCompletedException => ApiResponse<object>.ErrorResponse(
                    HttpStatusCode.Conflict,
                    exception.Message),
                
                // Excepciones genéricas del sistema
                ArgumentException => ApiResponse<object>.ErrorResponse(
                    HttpStatusCode.BadRequest,
                    "Parámetros inválidos: " + exception.Message),
                
                KeyNotFoundException => ApiResponse<object>.ErrorResponse(
                    HttpStatusCode.NotFound,
                    "Recurso no encontrado: " + exception.Message),
                
                UnauthorizedAccessException => ApiResponse<object>.ErrorResponse(
                    HttpStatusCode.Unauthorized,
                    "Acceso no autorizado: " + exception.Message),
                
                InvalidOperationException => ApiResponse<object>.ErrorResponse(
                    HttpStatusCode.BadRequest,
                    "Operación inválida: " + exception.Message),
                
                TimeoutException => ApiResponse<object>.ErrorResponse(
                    HttpStatusCode.RequestTimeout,
                    "Tiempo de espera agotado: " + exception.Message),
                
                _ => ApiResponse<object>.ErrorResponse(
                    HttpStatusCode.InternalServerError,
                    "Ha ocurrido un error interno del servidor")
            };

            context.Response.StatusCode = (int)response.StatusCode;

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var jsonResponse = JsonSerializer.Serialize(response, jsonOptions);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}