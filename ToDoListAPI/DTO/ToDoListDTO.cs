using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoListAPI.DTO
{
    public class ToDoListDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        // List of tasks associated with this ToDoList
        public List<TaskActivityDTO> Activities { get; set; } = new();
    }
    public class CreateToDoListDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }

    public class UpdateToDoListDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}

// DTOs for ToDoList
