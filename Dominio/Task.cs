using System.ComponentModel.DataAnnotations;

namespace LIST.TASK.DEMO.Dominio
{
    public class Task
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string? Description { get; set; }
        
        public bool IsCompleted { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime? CompletedDate { get; set; }
    }
}