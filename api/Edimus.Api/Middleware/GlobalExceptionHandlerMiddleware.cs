using Shared.Core.Exceptions;
using System.Net;
using System.Text.Json;

namespace Edimus.Api.Middleware;
public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    public GlobalExceptionHandlerMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var exceptionResponse = new ExceptionResponse();

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        exceptionResponse.ErrorMessages = new List<string> { exception.Message };
        exceptionResponse.InnerErrorMessages =
            exception.InnerException is null ? null :
            new List<string> { exception.InnerException.Message };

        if (exception is ArgumentException argumentException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            exceptionResponse.ErrorMessages = new List<string> { argumentException.Message };
        }

        if (exception is InvalidOperationException operationException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            exceptionResponse.ErrorMessages = new List<string> { operationException.Message };
        }

        if (exception is HttpBadRequestException badRequestException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            exceptionResponse.ErrorMessages = new List<string> { badRequestException.Message };
        }

        if (exception is HttpForbiddenException forbiddenException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            exceptionResponse.ErrorMessages = new List<string> { forbiddenException.Message };
        }

        if (exception is HttpNotFoundException notFoundException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            exceptionResponse.ErrorMessages = new List<string> { notFoundException.Message };
        }

        if (exception is HttpConflictException conflictException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            exceptionResponse.ErrorMessages = new List<string> { conflictException.Message };
        }

        exceptionResponse.Status = context.Response.StatusCode;
        var jsonContent = JsonSerializer.Serialize(exceptionResponse,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        return context.Response.WriteAsync(jsonContent);
    }
    public class ExceptionResponse
    {
        public int Status { get; set; }
        public List<string> ErrorMessages { get; set; } = new();
        public List<string>? InnerErrorMessages { get; set; }
    }
}
