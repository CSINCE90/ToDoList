using System;

namespace ToDoListAPI.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message) { }
    }
}

//409 conflict error message 