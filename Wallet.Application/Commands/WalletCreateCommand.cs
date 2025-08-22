using MediatR;
using Wallet.Application.Helpers;
using Wallet.Application.Inputs;
using Wallet.Application.Models;
using Wallet.Application.Outputs;
using Wallet.Infrastructure.UnitOfWork;

namespace Wallet.Application.Commands;

public class WalletCreateCommand(WalletCreateInput input) : IRequest<BaseResponse<WalletCreateOutput>>
{
    public WalletCreateInput Input { get; } = input;
}

public class WalletCreateCommandHandler(IUnityOfWork unityOfWork) : IRequestHandler<WalletCreateCommand, BaseResponse<WalletCreateOutput>>
{
    public async Task<BaseResponse<WalletCreateOutput>> Handle(WalletCreateCommand request, CancellationToken cancellationToken)
    {
        //TODO Check if wallet already exists for the user with tckn
        //TODO Card Service Implementation
        var wallet = new Domain.Entities.Wallet
        {
            UserId = request.Input.UserId,
            CardNumber = "AAAA-AAAA-AAAA-AAAA"
        };

        await unityOfWork.GetRepository<Domain.Entities.Wallet>().AddAsync(wallet);

        //TODO error handling

        var output = new WalletCreateOutput
        {
            WalletId = wallet.Id
        };
        return ResponseHelper.Success(output);
    }
}
