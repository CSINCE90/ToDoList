using System;

namespace ToDoListAPI.Exceptions
{
    /// <summary>
    /// Indica che la risorsa richiesta non è stata individuata dal sistema.
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}

//404 not found: for example list not found
