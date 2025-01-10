using Microsoft.AspNetCore.Mvc;
using PicPaySimplificado.Models.DTOs.Requests;
using PicPaySimplificado.Services.Transfer;

namespace PicPaySimplificado.Controllers;

[ApiController]
[Route("[controller]")]
public class TransferController : ControllerBase
{
    private readonly ITransferService _transferService;

    public TransferController(ITransferService transferService)
    {
        _transferService = transferService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransfer([FromBody] TransferRequest transferRequest)
    {
        var response = await _transferService.ExecuteAsync(transferRequest);
        
        if (response.IsSuccess)
            return Ok(response);
        
        return BadRequest(response);
    }
}