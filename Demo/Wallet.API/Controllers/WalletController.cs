using Microsoft.AspNetCore.Mvc;
using Wallet.API.Domain;
using Wallet.API.Models;

namespace Wallet.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WalletController : ControllerBase
{
    private readonly WalletDomainRepository _repository;

    public WalletController(WalletDomainRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{walletId}")]
    public async Task<IActionResult> GetWallet(Guid walletId)
    {
        var wallet = await _repository.GetAsync(walletId);

        return Ok(wallet);
    }

    [HttpPost]
    public async Task<IActionResult> CreateWallet(CreateWalletDto model)
    {
        var wallet = new Wallet.API.Domain.Wallet(model.UserId);
        
        await _repository.SaveAsync(wallet);
        return Ok(wallet);
    }
}
