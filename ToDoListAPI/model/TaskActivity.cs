using System.ComponentModel.DataAnnotations;

namespace ToDoListAPI.model
{

    
    public class TaskActivity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsCompleted { get; set; } = false;

        /// <summary>
        /// Indica se l'attività è stata eliminata logicamente (soft delete).
        /// </summary>
        public bool IsDeleted { get; set; } = false;

     
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Data dell'ultimo aggiornamento dell'attività.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

       
        [Required]
        public int ToDoListId { get; set; }

    
        [Required]
        public ToDoList ToDoList { get; set; } = null!; 
    }
}
