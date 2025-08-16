using AutoMapper;
using MediatR;
using Wallet.Application.Helpers;
using Wallet.Application.Inputs;
using Wallet.Application.Models;
using Wallet.Application.Outputs;
using Wallet.Infrastructure.UnitOfWork;

namespace Wallet.Application.Queries;

public class WalletByWalletIdQuery(WalletByWalletIdInput input) : IRequest<BaseResponse<WalletByWalletIdOutput>>
{
    public WalletByWalletIdInput Input { get; } = input;
}

public class WalletByWalletIdQueryHandler(IUnityOfWork unityOfWork, IMapper mapper) : IRequestHandler<WalletByWalletIdQuery, BaseResponse<WalletByWalletIdOutput>>
{
    public async Task<BaseResponse<WalletByWalletIdOutput>> Handle(WalletByWalletIdQuery request, CancellationToken cancellationToken)
    {
        var wallet = await unityOfWork.GetRepository<Domain.Entities.Wallet>().GetAsync(wallet => wallet.UserId == request.Input.UserId && wallet.Id == request.Input.WalletId);
        var mappedWallet = mapper.Map<WalletByWalletIdOutput>(wallet);
        return ResponseHelper.Success(mappedWallet);
    }
}