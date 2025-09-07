using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListAPI.model

{
    [Table("ToDoList")]
    public class ToDoList
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<TaskActivity> Activities { get; set; } = new List<TaskActivity>();
    }


}

//This class is the contenier for the TaskActivity 