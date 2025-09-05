using MassTransit;
using System.Threading;
using Wallet.Domain.Entities;
using Wallet.Domain.Events;
using Wallet.Integration.Services.CardService;

namespace Wallet.Integration.Consumers;

public class WalletCreatedConsumer(ICardService cardService, IBus bus) : IConsumer<WalletCreatedEvent>
{
    public async Task Consume(ConsumeContext<WalletCreatedEvent> context)
    {
        var response = await cardService.GetNewCard(context.Message.WalletId);
        if(response is null || string.IsNullOrEmpty(response.CardNumber))
        {
            //TODO: retry logic
            return;
        }

        await bus.Publish(new CardCreatedEvent { CardNumber = response.CardNumber, WalletId = context.Message.WalletId });
    }
}
