using System.Net;
using System.Text.Json;

namespace CryptoTrackerApi.Middleware
{
    public class ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unhandled exception occurred while processing the request.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.Clear();
            context.Response.ContentType = "application/json";

            var (statusCode, message) = exception switch
            {
                ArgumentNullException => (HttpStatusCode.BadRequest, "Required data is missing"),
                ArgumentException => (HttpStatusCode.BadRequest, exception.Message),
                InvalidOperationException => (HttpStatusCode.Conflict, exception.Message),
                HttpRequestException => (HttpStatusCode.ServiceUnavailable, "External service is unavailable"),
                _ => (HttpStatusCode.InternalServerError, "An error occurred while processing your request")
            };

            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                error = new
                {
                    message,
                    statusCode = (int)statusCode,
                    timestamp = DateTimeOffset.UtcNow
                }
            };

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
