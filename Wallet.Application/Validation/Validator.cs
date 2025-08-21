
using MediatR;
using System.Net;
using Wallet.Application.Exceptions;
using Wallet.Application.Inputs;
using Wallet.Application.Queries;

namespace Wallet.Application.Validation;

public class Validator(IMediator mediator) : IValidator
{
    public async Task Validate<TEntity>(TEntity response, string errorParamVal)
    {
        if (response == null)
        {
            var input = new GetParameterInput()
            {
                ParamType = "ErrorMessages",
                ParamValue = errorParamVal
            };
            var query = new GetParameterQuery(input);
            var paramResponse = await mediator.Send(query);

            string errorMessage;
            if (paramResponse == null || !paramResponse.Success || paramResponse.Data == null || paramResponse.Data.Parameters.Count == 0)
            {
                errorMessage = "An unexpected error occurred.";
            }
            else
            {
                var errorParameter = paramResponse.Data.Parameters.FirstOrDefault();
                errorMessage = errorParameter!.ParamDescription!;
            }

            throw new BaseCustomException(errorMessage, 400, HttpStatusCode.BadRequest);
        }
    }

    public async Task Validate<TEntity>(TEntity response, Func<TEntity, bool> predicate, string errorParamVal)
    {
        if (response == null || predicate(response))
        {
            var input = new GetParameterInput()
            {
                ParamType = "ErrorMessages",
                ParamValue = errorParamVal
            };
            var query = new GetParameterQuery(input);
            var paramResponse = await mediator.Send(query);

            string errorMessage;
            if (paramResponse == null || !paramResponse.Success || paramResponse.Data == null || paramResponse.Data.Parameters.Count == 0)
            {
                errorMessage = "An unexpected error occurred.";
            }
            else
            {
                var errorParameter = paramResponse.Data.Parameters.FirstOrDefault();
                errorMessage = errorParameter!.ParamDescription!;
            }

            throw new BaseCustomException(errorMessage, 400, HttpStatusCode.BadRequest);
        }
    }
}
