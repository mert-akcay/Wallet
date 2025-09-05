namespace Wallet.Domain.Events;

public class CardCreatedEvent
{
    public Guid WalletId { get; set; }
    public string CardNumber { get; set; }
}
