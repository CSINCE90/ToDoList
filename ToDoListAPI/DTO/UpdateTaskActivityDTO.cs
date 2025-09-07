using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoListAPI.DTO
{
    public class UpdateTaskActivityDTO
    {
        [Required]
        [MaxLength(120)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}