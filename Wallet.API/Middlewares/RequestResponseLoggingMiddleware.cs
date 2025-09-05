using Serilog;
using System.Diagnostics;
using System.Text;

namespace Wallet.API.Middlewares;

public class RequestResponseLoggingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var request = await FormatRequest(context.Request);

        var originalBodyStream = context.Response.Body;
        var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await next(context);

        responseBody.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(responseBody).ReadToEndAsync();

        Log.Information("Request: {Request}, Response: {Response}, Elapsed: {ElapsedTime}ms",
            request,
            $"Status: {context.Response.StatusCode}, Body: {responseText}",
            stopwatch.ElapsedMilliseconds);

        responseBody.Seek(0, SeekOrigin.Begin);
        await responseBody.CopyToAsync(originalBodyStream);
        context.Response.Body = originalBodyStream;
    }

    private async Task<string> FormatRequest(HttpRequest request)
    {
        request.EnableBuffering();
        using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
        var bodyAsText = await reader.ReadToEndAsync();
        request.Body.Position = 0;
        return $"{request.Method} {request.Path} {bodyAsText}";
    }
}