namespace ToDoListAPI.Exceptions;

/// <summary>
/// Eccezione di dominio lanciata quando i dati forniti dal client non superano la validazione.
/// Gestita dal <see cref="Middleware.GlobalExceptionHandler"/> restituendo un 400 Bad Request.
/// </summary>
public class ValidationException : Exception
{
    public ValidationException(string message) : base(message)
    {
    }
}
