using System;

namespace ToDoListAPI.Exceptions
{
    /// <summary>
    /// Exception for not found
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}

//404 not found: for example list not found