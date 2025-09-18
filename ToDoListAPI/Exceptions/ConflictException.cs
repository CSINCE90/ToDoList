using System;

namespace ToDoListAPI.Exceptions
{
    /// <summary>
    /// Exception for conflict
    /// </summary>
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message) { }
    }
}

//409 conflict: for example list is not empty 