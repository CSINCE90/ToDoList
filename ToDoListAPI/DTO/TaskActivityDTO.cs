using System;

namespace ToDoListAPI.DTO
{
    public class TaskActivityDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public int ToDoListId { get; set; }
    }
}