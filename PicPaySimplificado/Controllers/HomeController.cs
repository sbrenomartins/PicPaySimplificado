using Microsoft.AspNetCore.Mvc;

namespace PicPaySimplificado.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "Home")]
    public IActionResult Get()
    {
        return Ok();
    }
}