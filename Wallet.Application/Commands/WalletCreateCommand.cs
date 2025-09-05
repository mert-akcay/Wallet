using MassTransit;
using MediatR;
using Wallet.Application.Helpers;
using Wallet.Application.Inputs;
using Wallet.Application.Models;
using Wallet.Application.Outputs;
using Wallet.Domain.Events;
using Wallet.Infrastructure.Repositories.Interface;

namespace Wallet.Application.Commands;

public class WalletCreateCommand(WalletCreateInput input) : IRequest<BaseResponse<WalletCreateOutput>>
{
    public WalletCreateInput Input { get; } = input;
}

public class WalletCreateCommandHandler(IRepository<Wallet.Domain.Entities.Wallet> walletRepository, IBus bus) : IRequestHandler<WalletCreateCommand, BaseResponse<WalletCreateOutput>>
{
    public async Task<BaseResponse<WalletCreateOutput>> Handle(WalletCreateCommand request, CancellationToken cancellationToken)
    {
        //TODO Check if wallet already exists for the user with tckn
        //TODO Card Service Implementation
        var wallet = new Domain.Entities.Wallet
        {
            Id = Guid.NewGuid(),
            UserId = request.Input.UserId,
            CardNumber = "AAAA-AAAA-AAAA-AAAA"
        };

        await walletRepository.AddAsync(wallet);
        
        var output = new WalletCreateOutput
        {
            WalletId = wallet.Id
        };

        await bus.Publish(new WalletCreatedEvent { WalletId = wallet.Id }, cancellationToken);

        return ResponseHelper.Success(output);
    }
}
