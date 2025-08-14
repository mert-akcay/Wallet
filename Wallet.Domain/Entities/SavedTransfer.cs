namespace Wallet.Domain.Entities;

public class SavedTransfer : BaseEntity
{
    public Guid WalletId { get; set; }
    public Wallet Wallet { get; set; }

    public string Identifier { get; set; }

    public string DisplayName { get; set; }

    public string RecipientFullName { get; set; }

    public int BankCode { get; set; }
}
