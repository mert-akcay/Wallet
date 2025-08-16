namespace Wallet.Application.Outputs;

public class WalletByWalletIdOutput
{
    public Guid WalletId { get; set; }
    public Guid UserId { get; set; }
    public int WalletNumber { get; set; }
    public string CardNumber { get; set; }
    public decimal PayableBalance { get; set; }
    public decimal TransferableBalance { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
