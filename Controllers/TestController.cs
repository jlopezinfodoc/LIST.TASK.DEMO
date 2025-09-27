using Microsoft.AspNetCore.Mvc;
using LIST.TASK.DEMO.Servicios.Exceptions;

namespace LIST.TASK.DEMO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// Endpoint para probar diferentes tipos de excepciones
        /// </summary>
        /// <param name="exceptionType">Tipo de excepción a probar: notfound, validation, completed, argument, generic</param>
        /// <returns>Respuesta según la excepción</returns>
        [HttpGet("exception/{exceptionType}")]
        public IActionResult TestException(string exceptionType)
        {
            return exceptionType.ToLower() switch
            {
                "notfound" => throw new TaskNotFoundException(999),
                "validation" => throw new TaskValidationException("Error de validación de prueba"),
                "completed" => throw new TaskAlreadyCompletedException(123),
                "argument" => throw new ArgumentException("Argumento inválido de prueba"),
                "generic" => throw new Exception("Excepción genérica de prueba"),
                _ => Ok(new { message = "Tipos disponibles: notfound, validation, completed, argument, generic" })
            };
        }

        /// <summary>
        /// Endpoint que funciona correctamente
        /// </summary>
        /// <returns>Respuesta exitosa</returns>
        [HttpGet("success")]
        public IActionResult TestSuccess()
        {
            return Ok(new { message = "Todo funcionando correctamente", timestamp = DateTime.Now });
        }
    }
}