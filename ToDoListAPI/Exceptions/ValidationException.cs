using System;

namespace ToDoListAPI.Exceptions
{
    /// <summary>
    /// Eccezione di dominio lanciata quando i dati forniti dal client non superano la validazione.
    /// Viene intercettata dal <see cref="Filters.GlobalExceptionHandler"/> per restituire un
    /// "400 Bad Request" con dettagli leggibili all'interno della risposta ProblemDetails.
    /// </summary>
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
    }
}
