using MediatR;
using Wallet.Application.Helpers;
using Wallet.Application.Inputs;
using Wallet.Application.Models;
using Wallet.Application.Outputs;
using Wallet.Application.Queries;
using Wallet.Application.Validation;
using Wallet.Infrastructure.Repositories.Interface;

namespace Wallet.Application.Commands;

public class WalletUpdateCommand(WalletUpdateInput input) : IRequest<BaseResponse<WalletUpdateOutput>>
{
    public WalletUpdateInput Input { get; } = input;
}

public class WalletUpdateCommandHandler(IRepository<Wallet.Domain.Entities.Wallet> walletRepository, IMediator mediator, IValidator validator) : IRequestHandler<WalletUpdateCommand, BaseResponse<WalletUpdateOutput>>
{
    public async Task<BaseResponse<WalletUpdateOutput>> Handle(WalletUpdateCommand request, CancellationToken cancellationToken)
    {
        var walletRequest = new WalletByWalletIdInput
        {
            UserId = new Guid("afeadd0b-6bb8-4ab3-bbf4-180dcfe49101"),
            WalletId = request.Input.WalletId
        };
        var wallet = await mediator.Send(new WalletByWalletIdQuery(walletRequest), cancellationToken);
        await validator.Validate(wallet, "WalletCannotBeNull");

        var walletToUpdate = new Domain.Entities.Wallet
        {
            Id = request.Input.WalletId,
            UserId = wallet.Data.UserId,
            CardNumber = request.Input.CardNumber ?? wallet.Data.CardNumber,
            PayableBalance = request.Input.PayableBalance ?? wallet.Data.PayableBalance,
            TransferableBalance = request.Input.TransferableBalance ?? wallet.Data.TransferableBalance,
            CreatedAt = wallet.Data.CreatedAt,
            UpdatedAt = DateTime.UtcNow,
            DeletedAt = wallet.Data.DeletedAt,
            IsDeleted = wallet.Data.IsDeleted,
            WalletNumber = wallet.Data.WalletNumber
        };

        var updateResponse = walletRepository.Update(walletToUpdate);
        await validator.Validate(updateResponse, x => !x, "WalletCannotBeNull");

        return ResponseHelper.Success(new WalletUpdateOutput { IsSuccess = updateResponse } );
    }
}
