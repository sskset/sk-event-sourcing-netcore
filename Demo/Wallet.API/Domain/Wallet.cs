using SK.EventSourcing;
using System.Runtime.CompilerServices;

namespace Wallet.API.Domain;

public sealed class Wallet : SK.EventSourcing.AggregateRoot
{
    public Guid UserId { get; private set; }
    public decimal Balance { get; private set; }


    private Wallet() { }

    public static Wallet Create(Guid userId)
    {
        var wallet = new Wallet
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Balance = 0,
            Version = 0,
        };

        wallet.ApplyChange(new us)
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
        this.UserId = e.WalletCreated.UserId;
        this.Id = e.WalletCreated.Id;
        this.Balance = 0m;
        this.Version = e.WalletCreated.Version;
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
        public WalletCreated(Wallet walletCreated)
        {
            WalletCreated = walletCreated;
        }

        public Wallet WalletCreated { get; set; }
    }
}