using System.ComponentModel.DataAnnotations;

namespace TasksManagerWeb.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Task description is required")]
        [StringLength(500, ErrorMessage = "Task description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;
        
        public bool IsCompleted { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime? CompletedAt { get; set; }
    }
}