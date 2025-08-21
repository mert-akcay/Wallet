using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Wallet.Application.Exceptions;
using Wallet.Application.Helpers;

namespace Wallet.API.ExceptionHandler;

public class ExceptionHandler : IExceptionHandler
{

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is BaseCustomException customBaseException)
        {
            var customResponse = ResponseHelper.Fail<BaseCustomException>(customBaseException.Message, customBaseException.ErrorCode);

            httpContext.Response.StatusCode = (int)customBaseException.StatusCode;

            await httpContext.Response.WriteAsJsonAsync(customResponse, cancellationToken);
        }
        else
        {
            var defaultResponse = ResponseHelper.Fail<Exception>("An unexpected error occurred.", 500);

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await httpContext.Response.WriteAsJsonAsync(defaultResponse, cancellationToken);
        }

        return true;
    }
}
