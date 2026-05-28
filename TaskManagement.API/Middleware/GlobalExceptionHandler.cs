using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Common.Exceptions;

namespace TaskManagement.API.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An unhandled exception occurred: {message}", exception.Message);

        var (statusCode, title) = exception switch
        {
            ValidationException => (StatusCodes.Status400BadRequest, "Validation Failed"),
            NotFoundException => (StatusCodes.Status404NotFound, "Resource Not Found"),
            BadRequestException => (StatusCodes.Status400BadRequest, "Bad Request"),
            ArgumentException => (StatusCodes.Status400BadRequest, "Invalid Argument"),
            InvalidOperationException => (StatusCodes.Status400BadRequest, "Invalid Operation"),
            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
        };

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = statusCode == StatusCodes.Status500InternalServerError
                ? "An unexpected error occurred. Please try again later."
                : exception.Message,
            Instance = httpContext.Request.Path
        };

        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);
        if (exception is ValidationException validationException && validationException.Errors != null)
        {
            problemDetails.Extensions.Add("errors", validationException.Errors);
        }

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
