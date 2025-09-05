namespace Wallet.Application.Inputs;

public class WalletUpdateInput
{
    public Guid WalletId { get; set; }
    public string? CardNumber { get; set; }
    public decimal? PayableBalance { get; set; }
    public decimal? TransferableBalance { get; set; }
}
