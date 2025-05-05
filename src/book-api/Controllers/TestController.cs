using Microsoft.AspNetCore.Mvc;

namespace book_api.Controllers;

[ApiController]
[Route("v1/test")]
public class TestController() : ControllerBase
{
    [HttpPost]
    public IActionResult TestMethod([FromBody] List<string> body)
    {
        return Ok(body);
    }
}