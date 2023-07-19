using Man10UserServer.Common;
using Microsoft.AspNetCore.Mvc;

namespace Man10UserServer.Controllers;

[ApiController]
[Route("[controller]")]
public class ScoreController : ControllerBase
{

    [HttpGet("try")]
    public IActionResult TryConnect()
    {
        return StatusCode(200);
    }
    
    [HttpGet("get")]
    public int Get(string uuid)
    {
        return Score.Get(uuid).Result;
    }

    [HttpPost("give")]
    public IActionResult Give([FromBody] ScoreData data)
    {
        return StatusCode(Score.Give(data).Result);
    }

    [HttpPost("take")]
    public IActionResult Take([FromBody] ScoreData data)
    {
        return StatusCode(Score.Take(data).Result);
    }


}
public class ScoreData
{
    public string UUID { get; set; }
    public int Amount { get; set; }
    public string Note { get; set; }
    public string Issuer { get; set; }
}
