namespace Wallet.API.Models
{
    public class DepositDto
    {
        public Guid WalletId { get; set; }
        public decimal Amount { get; set; }
    }
}
