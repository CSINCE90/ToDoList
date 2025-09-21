using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoListAPI.DTO
{
    /// <summary>
    /// DTO per restituire una lista con il relativo insieme di attività.
    /// </summary>
    public class ToDoListDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        // List of tasks associated with this ToDoList
        public List<TaskActivityDTO> Activities { get; set; } = new();
    }

    /// <summary>
    /// Modello di input per creare una nuova lista to-do.
    /// </summary>
    public class CreateToDoListDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }

    /// <summary>
    /// Modello di input per aggiornare il nome di una lista esistente.
    /// </summary>
    public class UpdateToDoListDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
