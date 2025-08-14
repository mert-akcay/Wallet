namespace Wallet.Domain.Entities;

public class Wallet : SoftDeletableEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public int WalletNumber { get; set; }
    public string CardNumber { get; set; }

    public decimal PayableBalance { get; set; }
    public decimal TransferableBalance { get; set; }

    public ICollection<Transaction> Transactions { get; set; }
    public ICollection<SavedTransfer> SavedTransfers { get; set; }
}
