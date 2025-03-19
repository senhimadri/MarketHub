using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.ApiGateway
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            await LogRequest(httpContext);

            // Capture the response
            var originalBodyStream = httpContext.Response.Body;
            using var responseBody = new MemoryStream();
            httpContext.Response.Body = responseBody;

            try
            {
                // Call the next middleware in the pipeline
                await _next(httpContext);

                // Log the response
                await LogResponse(httpContext, responseBody);
            }
            finally
            {
                // Copy the response to the original stream and restore it
                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
                httpContext.Response.Body = originalBodyStream;
            }
        }


        private async Task LogRequest(HttpContext context)
        {
            try
            {
                var request = context.Request;
                request.EnableBuffering(); // Allow reading the body multiple times

                // Read the request body
                var body = string.Empty;
                if (request.Body.CanRead)
                {
                    using var reader = new StreamReader(
                        request.Body,
                        encoding: Encoding.UTF8,
                        detectEncodingFromByteOrderMarks: false,
                        leaveOpen: true);

                    body = await reader.ReadToEndAsync();
                    request.Body.Position = 0; // Reset the position for downstream middleware
                }

                // Log request details
                _logger.LogInformation(
                    "Incoming Request: {Method} {Path} {QueryString} | Headers: {@Headers} | Body: {Body}",
                    request.Method,
                    request.Path,
                    request.QueryString,
                    request.Headers,
                    body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error logging request");
            }
        }

        private async Task LogResponse(HttpContext context, MemoryStream responseBody)
        {
            try
            {
                responseBody.Seek(0, SeekOrigin.Begin);
                var body = await new StreamReader(responseBody).ReadToEndAsync();
                responseBody.Seek(0, SeekOrigin.Begin);

                // Log response details

                _logger.LogInformation("Got the response");
                //_logger.LogInformation(
                //    "Outgoing Response: {StatusCode} | Headers: {@Headers} | Body: {Body}",
                //    context.Response.StatusCode,
                //    context.Response.Headers,
                //    body);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error logging response");
            }
        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}
