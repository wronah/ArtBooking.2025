using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet()]
    public ActionResult Check()
    {
        return Ok(new
        {
            Message = "Check successfull"
        });
    }
}