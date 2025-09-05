using MassTransit;
using MediatR;
using Wallet.Application.Inputs;
using Wallet.Domain.Events;

namespace Wallet.Application.Consumers;

public class CardCreatedConsumer(IMediator mediator) : IConsumer<CardCreatedEvent>
{
    public async Task Consume(ConsumeContext<CardCreatedEvent> context)
    {
        var request = new WalletUpdateInput
        {
            WalletId = context.Message.WalletId,
            CardNumber = context.Message.CardNumber,
        };

        await mediator.Send(new Commands.WalletUpdateCommand(request));
    }
}
