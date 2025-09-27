
using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.Exceptions;

namespace ToDoListAPI.Filters;

/// <summary>
/// Middleware che intercetta tutte le eccezioni propagate dalla pipeline HTTP
/// e le trasforma in risposte JSON compatibili con <see cref="ProblemDetails"/>.
/// Mappa le eccezioni di dominio (validazione, not found, conflict) ai rispettivi
/// status code, logga l'evento e restituisce un payload coerente al client.
/// </summary>
public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, title) = MapException(exception);

        if (statusCode >= StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);
        }
        else
        {
            _logger.LogWarning(exception, "Handled domain exception: {Message}", exception.Message);
        }

        var problem = new ProblemDetails
        {
            Title = title,
            Detail = exception.Message,
            Status = statusCode,
            Instance = context.Request.Path
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(problem);
    }

    private static (int StatusCode, string Title) MapException(Exception exception) => exception switch
    {
        ValidationException => (StatusCodes.Status400BadRequest, "Validation Error"),
        NotFoundException => (StatusCodes.Status404NotFound, "Not Found"),
        ConflictException => (StatusCodes.Status409Conflict, "Conflict"),
        _ => (StatusCodes.Status500InternalServerError, "Server Error")
    };
}
