using Microsoft.AspNetCore.Mvc;
using Wallet.API.Domain;
using Wallet.API.Models;

namespace Wallet.API.Controllers;

[ApiController]
[Route("wallet")]
public class WalletController : ControllerBase
{
    private readonly WalletDomainRepository _repository;

    public WalletController(WalletDomainRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{walletId}")]
    public IActionResult GetWallet(Guid walletId)
    {
        var wallet = _repository.Get(walletId);

        return Ok(wallet);
    }

    [HttpPost]
    public IActionResult CreateWallet(CreateWalletDto model)
    {
        var wallet = Wallet.API.Domain.Wallet.Create(model.UserId);
        
        _repository.Save(wallet);
        return Ok(wallet);
    }

    [HttpPost("{walletId}/deposit")]
    public IActionResult Deposit(DepositDto model)
    {
        var wallet = _repository.Get(model.WalletId);
        wallet.Deposit(model.Amount);

        _repository.Save(wallet);
        return Ok(wallet);      
    }

    [HttpPost("{walletId}/withdraw")]
    public IActionResult Withdraw(DepositDto model)
    {
        var wallet = _repository.Get(model.WalletId);
        wallet.Withdraw(model.Amount);
        _repository.Save(wallet);

        return Ok(wallet);
    }
}
