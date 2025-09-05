using AutoMapper;
using MediatR;
using Wallet.Application.Helpers;
using Wallet.Application.Inputs;
using Wallet.Application.Models;
using Wallet.Application.Outputs;
using Wallet.Application.Validation;
using Wallet.Infrastructure.Repositories.Interface;

namespace Wallet.Application.Queries;

public class GetParameterQuery(GetParameterInput input) : IRequest<BaseResponse<GetParameterOutput>>
{
    public GetParameterInput Input { get; } = input;
}

public class GetParameterQueryHandler(IRepository<Domain.Entities.Parameter> parameterRepository, IMapper mapper, IValidator validator) : IRequestHandler<GetParameterQuery, BaseResponse<GetParameterOutput>>
{
    public async Task<BaseResponse<GetParameterOutput>> Handle(GetParameterQuery request, CancellationToken cancellationToken)
    {
        var parameter = await parameterRepository
            .FindAsync(p => p.ParamType == request.Input.ParamType &&
            (request.Input.ParamValue == null || p.ParamValue == request.Input.ParamValue));

        await validator.Validate(parameter, p => p == null || !p.Any(), "ParameterNotFound");

        var output = new GetParameterOutput();
        var mappedParameter = mapper.Map<List<GetParameterModel>>(parameter);
        output.Parameters = mappedParameter;
        return ResponseHelper.Success(output);
    }
}
