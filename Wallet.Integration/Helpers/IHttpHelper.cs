namespace Wallet.Integration.Helpers;

public interface IHttpHelper
{
    public HttpRequestMessage PrepareHttpRequest(
        HttpClient client,
        HttpMethod method,
        string url,
        object? body = null,
        Dictionary<string, string>? headers = null);

    public Task<T?> SendAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken = default);
}
