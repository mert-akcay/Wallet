using Wallet.Application.Models;

namespace Wallet.Application.Helpers;

public static class ResponseHelper
{
    public static BaseResponse<T> Success<T>(T? data) where T : class
    {
        return new BaseResponse<T>(data);
    }
    public static BaseResponse<T> Fail<T>(string errorMessage, int errorCode) where T : class
    {
        return new BaseResponse<T>(errorCode, errorMessage);
    }
}
