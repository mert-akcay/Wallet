
namespace Wallet.Application.Models;

public class BaseResponse<T> where T : class
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public int? ErrorCode { get; set; }
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
