using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI.data;
using ToDoListAPI.model;
using ToDoListAPI.service;

namespace ToDoListAPI.DTO
{
    public class ToDoListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}