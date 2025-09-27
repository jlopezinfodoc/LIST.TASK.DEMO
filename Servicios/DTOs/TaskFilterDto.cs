namespace LIST.TASK.DEMO.Servicios.DTOs
{
    public class TaskFilterDto
    {
        public bool? IsCompleted { get; set; }
        public string? Title { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}