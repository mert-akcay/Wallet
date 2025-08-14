namespace Wallet.Domain.Entities;

public class Transaction : SoftDeletableEntity
{
    public Guid WalletId { get; set; }
    public Wallet Wallet { get; set; }

    public Guid ReferenceId { get; set; }
    public int TransactionType { get; set; }
    public int TransactionForm { get; set; }

    public decimal PayableInitialBalance { get; set; }
    public decimal PayableAmount { get; set; }

    public decimal TransferableInitialBalance { get; set; }
    public decimal TransferableAmount { get; set; }

    public int Status { get; set; }

    public Guid TransactionBankInfoId { get; set; }
    public TransactionBankInfo TransactionBankInfo { get; set; }

    public string Description { get; set; }

    public Guid ReceiptId { get; set; }
    public Receipt Receipt { get; set; }
}
