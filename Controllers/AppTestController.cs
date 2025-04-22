using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

[Route("api/[controller]")]
[ApiController]

public class ApptestController : ControllerBase
{
    public ApptestController() { }

    [HttpGet]
    [EnableRateLimiting("fixed")]
    public IActionResult getTest()
    {
        var result = new Dictionary<string, object>();
        result.Add("success", true);
        result.Add("message", "Get Test");

        return Ok(result);
    }
}