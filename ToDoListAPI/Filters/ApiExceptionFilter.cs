using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ToDoListAPI.Exceptions;

namespace ToDoListAPI.Filters
{
    /// <summary>
    /// Converte le eccezioni di dominio in risposte HTTP standard (ProblemDetails).
    /// </summary>
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            var (status, title) = ex switch
            {
                ValidationException => (StatusCodes.Status400BadRequest, "Validation Error"),
                NotFoundException => (StatusCodes.Status404NotFound, "Not Found"),
                ConflictException => (StatusCodes.Status409Conflict, "Conflict"),
                _ => (StatusCodes.Status500InternalServerError, "Server Error")
            };

            if (status >= 500)
            {
                _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
            }
            else
            {
                _logger.LogWarning(ex, "Handled domain exception: {Message}", ex.Message);
            }

            var problem = new ProblemDetails
            {
                Title = title,
                Detail = ex.Message,
                Status = status,
                Instance = context.HttpContext.Request.Path
            };

            context.Result = new ObjectResult(problem) { StatusCode = status };
            context.ExceptionHandled = true;
        }
    }
}

