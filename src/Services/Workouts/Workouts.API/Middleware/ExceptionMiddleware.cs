using System.Net;
using System.Text.Json;
using Workouts.BusinessLogic.Exceptions;

namespace Workouts.API.Middleware
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

        /// <summary>
        /// Invokes the middleware to handle exceptions.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
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

        /// <summary>
        ///  Handles an exception by returning an appropriate JSON response.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <param name="exception">The exception to be handled.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
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

        /// <summary>
        /// Determines the HTTP status code based on the type of exception.
        /// </summary>
        /// <param name="exception">The exception to analyze.</param>
        /// <returns>The appropriate HTTP status code.</returns>
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
