using System.Collections.Generic;
using ToDoListAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI.data;
using ToDoListAPI.model;
using ToDoListAPI.service;
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

// DTO (Data Transfer Objects) for ToDoList
// - ToDoListDTO: Used to return a ToDoList from the API (includes Id and Name).
// - CreateToDoListDTO: Used when creating a new ToDoList. Only requires the Name property.
// - UpdateToDoListDTO: Used when updating an existing ToDoList. Only requires the Name property.
// 
// These classes separate the API contract from the database entity,
// ensuring that clients only send/receive the data that makes sense for each operation.