using AutoMapper;
using MediatR;
using Wallet.Application.Helpers;
using Wallet.Application.Inputs;
using Wallet.Application.Models;
using Wallet.Application.Outputs;
using Wallet.Application.Validation;
using Wallet.Infrastructure.UnitOfWork;

namespace Wallet.Application.Queries;

public class WalletByWalletIdQuery(WalletByWalletIdInput input) : IRequest<BaseResponse<WalletByWalletIdOutput>>
{
    public WalletByWalletIdInput Input { get; } = input;
}

public class WalletByWalletIdQueryHandler(IUnityOfWork unityOfWork, IMapper mapper, IValidator validator) : IRequestHandler<WalletByWalletIdQuery, BaseResponse<WalletByWalletIdOutput>>
{
    public async Task<BaseResponse<WalletByWalletIdOutput>> Handle(WalletByWalletIdQuery request, CancellationToken cancellationToken)
    {
        var walletResponse = await unityOfWork.GetRepository<Domain.Entities.Wallet>().GetFirstOrDefaultAsync(wallet => wallet.UserId == request.Input.UserId && wallet.Id == request.Input.WalletId);

        await validator.Validate(walletResponse, "WalletCannotBeNull");

        var mappedWallet = mapper.Map<WalletByWalletIdOutput>(walletResponse);
        return ResponseHelper.Success(mappedWallet);
    }
}