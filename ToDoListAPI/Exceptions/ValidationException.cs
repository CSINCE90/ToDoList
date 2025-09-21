using System;

namespace ToDoListAPI.Exceptions
{
    /// <summary>
    /// Rappresenta errori di validazione sugli input dell'applicazione.
    /// </summary>
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
    }
}
//400 bad request
