using System;

namespace ToDoListAPI.Exceptions
{
    /// <summary>
    /// Exception for validation
    /// </summary>
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
    }
}
//400 bad request