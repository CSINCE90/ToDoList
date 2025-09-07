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
    }
}