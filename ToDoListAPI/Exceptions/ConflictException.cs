using System;

namespace ToDoListAPI.Exceptions
{
    /// <summary>
    /// Segnala conflitti di dominio, come violazioni di vincoli o stati non compatibili.
    /// </summary>
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message) { }
    }
}

//409 conflict: for example list is not empty 
