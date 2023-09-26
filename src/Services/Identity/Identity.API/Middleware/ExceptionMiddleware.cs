using Identity.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace Identity.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status = GetStatusCode(exception);
            string message = exception.Message;
            string? stackTrace = exception.StackTrace;

            var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            if (status == HttpStatusCode.InternalServerError)
            {
                _logger.LogError(exception, message);
            }

            return context.Response.WriteAsync(exceptionResult);
        }
        private static HttpStatusCode GetStatusCode(Exception exception) => exception switch
        {
            BadRequestException => HttpStatusCode.BadRequest,
            NotFoundException => HttpStatusCode.NotFound,
            AlreadyExistsException => HttpStatusCode.Conflict,
            NotImplementedException => HttpStatusCode.NotImplemented,
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            _ => HttpStatusCode.InternalServerError,
        };
    }
}
