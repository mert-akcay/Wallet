using Serilog;
using System.Diagnostics;
using System.Text;

namespace Wallet.API.Middlewares;

public class RequestResponseLoggingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var request = FormatRequest(context.Request);

        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await next(context);

        var response = await FormatResponse(context.Response);
        stopwatch.Stop();
        var elapsedTime = stopwatch.ElapsedMilliseconds;


        Log.Information("Request: {Request}, Response: {Response}, Elapsed: {ElapsedTime}", request, response, elapsedTime);

        await responseBody.CopyToAsync(originalBodyStream);
    }

    private string FormatRequest(HttpRequest request)
    {
        request.EnableBuffering();
        var buffer = new byte[Convert.ToInt32(request.ContentLength)];
        var bodyAsText = Encoding.UTF8.GetString(buffer);
        request.Body.Position = 0;
        return $"{request.Method} {request.Path} {bodyAsText}";
    }

    private async Task<string> FormatResponse(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        var text = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);
        return $"Status Code: {response.StatusCode}, Body: {text}";
    }
}