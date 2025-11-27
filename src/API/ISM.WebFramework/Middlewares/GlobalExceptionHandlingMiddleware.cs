using ISM.SharedKernel.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ISM.WebFramework.Middlewares;

public sealed class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
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
        var problemDetails = CreateProblemDetails(context, exception);

        _logger.LogError(
            exception,
            "Unhandled exception occurred. StatusCode: {StatusCode}, TraceId: {TraceId}",
            problemDetails.Status,
            context.TraceIdentifier);

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails, options));
    }

    private static ProblemDetails CreateProblemDetails(HttpContext context, Exception exception)
    {
        var (statusCode, title, errors) = MapException(exception);

        var problemDetails = new ProblemDetails
        {
            Type = $"https://httpstatuses.com/{statusCode}",
            Title = title,
            Status = statusCode,
            Detail = exception.Message,
            Instance = context.Request.Path
        };

        problemDetails.Extensions["traceId"] = context.TraceIdentifier;

        if (errors?.Any() == true)
        {
            problemDetails.Extensions["errors"] = errors;
        }

        return problemDetails;
    }

    private static (int StatusCode, string Title, IDictionary<string, string[]>? Errors) MapException(Exception exception)
    {
        return exception switch
        {
            ValidationException validationException => (StatusCodes.Status400BadRequest, "Validation error", validationException.Errors),
            BadRequestException badRequest => (StatusCodes.Status400BadRequest, "Bad request", badRequest.Errors),
            UnauthorizedException => (StatusCodes.Status401Unauthorized, "Unauthorized", null),
            ForbiddenException => (StatusCodes.Status403Forbidden, "Forbidden", null),
            NotFoundException => (StatusCodes.Status404NotFound, "Not Found", null),
            ConflictException => (StatusCodes.Status409Conflict, "Conflict", null),
            BusinessRuleViolationException or DomainException => (StatusCodes.Status422UnprocessableEntity, "Unprocessable Entity", null),
            AppException appException => (StatusCodes.Status400BadRequest, "Bad request", appException.Errors),
            _ => (StatusCodes.Status500InternalServerError, "Server Error", null)
        };
    }
}
