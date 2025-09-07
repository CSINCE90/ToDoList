using System;

namespace ToDoListAPI.DTO
{
    public class TaskActivityDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public int ToDoListId { get; set; }
    }
}