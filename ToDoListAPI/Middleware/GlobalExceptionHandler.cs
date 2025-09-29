using Microsoft.AspNetCore.Mvc; // per ProblemDetails
using ToDoListAPI.Exceptions; // per le eccezioni di dominio
using System.Diagnostics; // Activity per TraceId

namespace ToDoListAPI.Middleware;

/// <summary>
/// Middleware per gestire le eccezioni di dominio
/// </summary>
public sealed class GlobalExceptionHandler
{
    private readonly RequestDelegate _next; // delegato per il successivo middleware
    private readonly ILogger<GlobalExceptionHandler> _logger; // per il logging 

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
            await HandleExceptionAsync(context, ex); // gestisce l'eccezione e restituisce la risposta
        }
    }


    /// <summary>
    /// Gestisce l'eccezione e restituisce la risposta
    /// </summary>
    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var (statusCode, title) = MapException(ex);

        if (statusCode >= StatusCodes.Status500InternalServerError)
            _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
        else
            _logger.LogWarning(ex, "Handled domain exception: {Message}", ex.Message);

        if (context.Response.HasStarted) return;

        var problem = new ProblemDetails
        {
            Title = title,
            Detail = ex.Message,
            Status = statusCode,
            Instance = context.Request.Path
        };

        problem.Extensions["traceId"] = Activity.Current?.Id ?? context.TraceIdentifier;

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(problem);
    }

    private static (int StatusCode, string Title) MapException(Exception ex) => ex switch
    {
        ValidationException => (StatusCodes.Status400BadRequest, "Validation Error"),
        NotFoundException => (StatusCodes.Status404NotFound, "Not Found"),
        ConflictException => (StatusCodes.Status409Conflict, "Conflict"),
        _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
    };
}






