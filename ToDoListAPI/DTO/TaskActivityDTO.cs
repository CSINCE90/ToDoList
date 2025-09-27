using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoListAPI.DTO
{
    /// <summary>
    /// DTO per esporre le informazioni principali di una TaskActivity tramite API.
    /// </summary>
    public class TaskActivityDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(120)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public int ToDoListId { get; set; }
    }
    
    /// <summary>
    /// Input model per la creazione di una TaskActivity.
    /// </summary>
    public class CreateTaskActivityDTO
    {
        [Required]
        [MaxLength(120)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        public DateTime? DueDate { get; set; }

        public int ToDoListId { get; set; }
    }

        /// <summary>
        /// Input model per la modifica di una TaskActivity.
        /// </summary>

        public class UpdateTaskActivityDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? IsCompleted { get; set; }
    }
    


}
