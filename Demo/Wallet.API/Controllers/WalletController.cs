using Microsoft.AspNetCore.Mvc;

namespace Wallet.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WalletController : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetWallets()
    {

    }

    [HttpGet("{id}")]
    public IActionResult GetWallet(int walletId)
    {
        return Ok();
    }
}
