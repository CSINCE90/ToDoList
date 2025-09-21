using System;

namespace ToDoListAPI.DTO
{
    /// <summary>
    /// Modello di input per modificare i dettagli di una TaskActivity esistente.
    /// I campi non valorizzati vengono lasciati invariati.
    /// </summary>
    public class UpdateTaskActivityDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
