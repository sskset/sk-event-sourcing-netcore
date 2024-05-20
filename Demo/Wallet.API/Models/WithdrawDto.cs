namespace Wallet.API.Models
{
    public class WithdrawDto
    {
        public Guid WalletId { get; set; }
        public decimal Amount { get; set; }
    }
}
