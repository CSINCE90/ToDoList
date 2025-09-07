using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListAPI.model
{
    public class TaskActivity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsCompleted { get; set; } = false;

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public int ToDoListId { get; set; }

        public ToDoList ToDoList { get; set; }
    }
}

//this class is designed with the supermarket shopping list in mind. It can be easily adapted to any activity to be carried out.