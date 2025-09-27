using System.ComponentModel.DataAnnotations;

namespace LIST.TASK.DEMO.Servicios.DTOs
{
    public class UpdateTaskDto
    {
        [Required(ErrorMessage = "El título es requerido")]
        [StringLength(200, ErrorMessage = "El título no puede exceder 200 caracteres")]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
        public string? Description { get; set; }
        
        public bool IsCompleted { get; set; }
    }
}