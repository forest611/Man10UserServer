using Man10UserServer.Common;
using Microsoft.AspNetCore.Mvc;

namespace Man10UserServer.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerController : ControllerBase
{
    [HttpGet("mcid")]
    public string GetMinecraftId(string uuid)
    {
        return Player.GetMinecraftId(uuid).Result;
    }

    [HttpGet("uuid")]
    public string GetUUID(string minecraftId)
    {
        return Player.GetUUID(minecraftId).Result;
    }
}