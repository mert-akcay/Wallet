namespace Wallet.Application.Inputs
{
    public class WalletByWalletIdInput
    {
        public Guid WalletId { get; set; }
        public Guid UserId { get; set; }
    }
}
