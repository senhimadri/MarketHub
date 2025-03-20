using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Serilog;

namespace MarketHub.ApiGateway
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RequestResponseLoggingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next= next;
        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            // Log the Request
            var requestBody = await ReadRequestBody(context.Request);
            var requestLog = new
            {
                Method = context.Request.Method,
                Url = context.Request.Path,
                Headers = context.Request.Headers,
                Body = requestBody
            };
            Log.Information("➡️ Request: {Request}", JsonSerializer.Serialize(requestLog));

            // Copy original response body  
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context); // Call next middleware

            stopwatch.Stop();

            // Log the Response
            var responseBodyContent = await ReadResponseBody(context.Response);
            var responseLog = new
            {
                StatusCode = context.Response.StatusCode,
                ElapsedTimeMs = stopwatch.ElapsedMilliseconds,
                Body = responseBodyContent
            };
            Log.Information("⬅️ Response: {Response}", JsonSerializer.Serialize(responseLog));

            // Reset Response Body
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }

        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.EnableBuffering();
            using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0;
            return body;
        }

        private async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(response.Body, Encoding.UTF8);
            var body = await reader.ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return body;
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
