using SK.EventSourcing;

namespace Wallet.API.Domain;

public sealed class Wallet : SK.EventSourcing.AggregateRoot
{
    public Guid UserId { get; private set; }
    public decimal Balance { get; private set; }


    public Wallet() { }

    public static Wallet Create(Guid userId)
    {
        var wallet = new Wallet();  
        wallet.ApplyChange(new WalletCreated(userId, Guid.NewGuid()));

        return wallet;
    }

    public void Deposit(decimal amount)
    {
        if(amount <= 0)
        {
            throw new Exception("Wallet: Deposit amount must be greater than $1.");
        }

        this.ApplyChange(new DepositWallet(amount));
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new Exception("Wallet: Withdraw amount must be greater than $1.");
        }

        if(amount > this.Balance)
        {
            // if exceed limit, we withdraw all instead
            amount = this.Balance;
        }

        this.ApplyChange(new WithdrawWallet(amount));
    }

    private void Apply(WithdrawWallet e)
    {
        this.Balance -= e.Amount;
    }

    private void Apply(DepositWallet e)
    {
        this.Balance += e.Amount;
    }

    private void Apply(WalletCreated e)
    {
        this.UserId = e.UserId;
        this.Id = e.WalletId;
        this.Balance = 0m;
    }

    public class WithdrawWallet : DomainEvent
    {
        public decimal Amount { get; set; }

        public WithdrawWallet(decimal amount)
        {
            Amount = amount;
        }
    }


    public class DepositWallet : DomainEvent
    {
        public decimal Amount { get; set; }

        public DepositWallet(decimal amount)
        {
            Amount = amount;
        }
    }

    public class WalletCreated : DomainEvent
    {
        public WalletCreated(Guid userId, Guid walletId)
        {
            UserId = userId;
            WalletId = walletId;
        }

        public Guid UserId { get; set; }
        public Guid WalletId { get; set; }
    }
}