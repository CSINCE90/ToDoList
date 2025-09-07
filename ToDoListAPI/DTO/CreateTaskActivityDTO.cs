using System;

namespace ToDoListAPI.DTO
{
    public class CreateTaskActivityDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int ToDoListId { get; set; }
    }
}