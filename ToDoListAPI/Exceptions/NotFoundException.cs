namespace ToDoListAPI.Exceptions;

/// <summary>
/// Eccezione di dominio lanciata quando la risorsa richiesta non è presente nel sistema.
/// Gestita dal <see cref="Middleware.GlobalExceptionHandler"/> restituendo un 404 Not Found.
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}
