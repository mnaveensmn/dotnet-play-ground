using webapi_play_ground.Services;

namespace webapi_play_ground.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ExceptionController
{
    private IService _service;
    
    public ExceptionController(IService service) => _service = service;

    [HttpPatch(Name = "Global Exception Handler Demo")]
    public string Get()
    {
        return _service.show();
    }
}