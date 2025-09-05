using AutoMapper;
using MediatR;
using Wallet.Application.Helpers;
using Wallet.Application.Inputs;
using Wallet.Application.Models;
using Wallet.Application.Outputs;
using Wallet.Application.Validation;
using Wallet.Infrastructure.Repositories.Interface;

namespace Wallet.Application.Queries;

public class WalletByWalletIdQuery(WalletByWalletIdInput input) : IRequest<BaseResponse<WalletByWalletIdOutput>>
{
    public WalletByWalletIdInput Input { get; } = input;
}

public class WalletByWalletIdQueryHandler(IRepository<Domain.Entities.Wallet> walletRepository, IMapper mapper, IValidator validator) : IRequestHandler<WalletByWalletIdQuery, BaseResponse<WalletByWalletIdOutput>>
{
    public async Task<BaseResponse<WalletByWalletIdOutput>> Handle(WalletByWalletIdQuery request, CancellationToken cancellationToken)
    {
        var walletResponse = await walletRepository.GetFirstOrDefaultAsync(wallet => wallet.UserId == request.Input.UserId && wallet.Id == request.Input.WalletId);

        await validator.Validate(walletResponse, "WalletCannotBeNull");

        var mappedWallet = mapper.Map<WalletByWalletIdOutput>(walletResponse);
        return ResponseHelper.Success(mappedWallet);
    }
}