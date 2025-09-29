namespace ToDoListAPI.Exceptions;

/// <summary>
/// Eccezione di dominio lanciata quando si verifica un conflitto con lo stato corrente della risorsa.
/// Gestita dal <see cref="Middleware.GlobalExceptionHandler"/> restituendo un 409 Conflict.
/// </summary>
public class ConflictException : Exception
{
    public ConflictException(string message) : base(message)
    {
    }
}
