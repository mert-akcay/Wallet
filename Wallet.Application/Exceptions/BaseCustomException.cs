using System.Net;

namespace Wallet.Application.Exceptions;

public class BaseCustomException : Exception
{
    public int ErrorCode { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public BaseCustomException(string message, int errorCode, HttpStatusCode statusCode)
     : base(message)
    {
        ErrorCode = errorCode;
        StatusCode = statusCode;
    }

    public BaseCustomException(string message, int errorCode, HttpStatusCode statusCode, Exception innerException)
     : base(message, innerException)
    {
        ErrorCode = errorCode;
        StatusCode = statusCode;
    }
}
