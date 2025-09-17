using System;

namespace ToDoListAPI.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
    }
}
//400 bad request error message 