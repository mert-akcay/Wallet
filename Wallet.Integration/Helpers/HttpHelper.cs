using System.Text;
using System.Text.Json;

namespace Wallet.Integration.Helpers;

public class HttpHelper : IHttpHelper
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializerOptions;

    public HttpHelper(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };
    }

    /// <summary>
    /// Prepares an HttpRequestMessage with method, url, headers, and optional body.
    /// </summary>
    public HttpRequestMessage PrepareHttpRequest(
        HttpClient client,
        HttpMethod method,
        string url,
        object? body = null,
        Dictionary<string, string>? headers = null)
    {
        var request = new HttpRequestMessage(method, client.BaseAddress + url);

        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        if (body != null)
        {
            var json = JsonSerializer.Serialize(body, _serializerOptions);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        return request;
    }

    /// <summary>
    /// Sends the request, gets the response as JSON, and deserializes it into T.
    /// </summary>
    public async Task<T?> SendAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(json))
            return default;

        return JsonSerializer.Deserialize<T>(json, _serializerOptions);
    }

}
