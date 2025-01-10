using Microsoft.AspNetCore.Mvc;
using PicPaySimplificado.Models.DTOs.Requests;
using PicPaySimplificado.Services.Wallet;

namespace PicPaySimplificado.Controllers;

[ApiController]
[Route("[controller]")]
public class WalletController : ControllerBase
{
    private readonly IWalletService _walletService;

    public WalletController(IWalletService walletService)
    {
        _walletService = walletService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWallet([FromBody] WalletRequest request)
    {
        var response = await _walletService.ExecuteAsync(request);
        
        if (response.IsSuccess)
            return Created();
        
        return BadRequest(response);
    }
}