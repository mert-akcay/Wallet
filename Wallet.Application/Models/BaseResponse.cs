
using System.Text.Json.Serialization;

namespace Wallet.Application.Models;

public class BaseResponse<T> where T : class
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; }
    public bool Success { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ErrorCode { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ErrorMessage { get; set; }

    public BaseResponse()
    {
        Success = true;
    }

    public BaseResponse(T? data)
    {
        Data = data;
        Success = true;
    }

    public BaseResponse(int errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }
}
