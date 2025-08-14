namespace Wallet.Domain.Entities;

public class Receipt : BaseEntity
{
    public Guid TransactionId { get; set; }
    public Transaction Transaction { get; set; }

    public string NumberPrefix { get; set; }

    public string Number { get; set; }

    public string Content { get; set; }
}
