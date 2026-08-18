using System.Diagnostics;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        Console.WriteLine($"Request: {context.Request.Path}");
        await _next(context);
        stopwatch.Stop();
        Console.WriteLine($"Duración: {stopwatch.ElapsedMilliseconds}ms");
        Console.WriteLine($"Response: {context.Response.StatusCode}");

        Console.WriteLine($"Método: {context.Request.Method}");
        Console.WriteLine($"IP: {context.Connection.RemoteIpAddress}");
        Console.WriteLine($"User-Agent: {context.Request.Headers["User-Agent"]}");
    }
}

public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app)
        => app.UseMiddleware<RequestLoggingMiddleware>();
}