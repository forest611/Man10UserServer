using Man10UserServer.Controllers;
using Man10UserServer.Models;

namespace Man10UserServer.Common;

public class Player
{
    /// <summary>
    /// MinecraftIDを取得する
    /// </summary>
    /// <param name="uuid"></param>
    /// <returns></returns>
    public static async Task<string> GetMinecraftId(string uuid)
    {
        var result = await Task.Run(() =>
        {
            var context = new SystemContext();
            var userName = context.player_data.FirstOrDefault(r => r.uuid == uuid)?.mcid;
            context.Dispose();
            return userName ?? "";
        });
        
        return result;
    }

    /// <summary>
    /// UUIDを取得する
    /// </summary>
    /// <param name="uuid"></param>
    /// <returns></returns>
    public static async Task<string> GetUUID(string minecraftId)
    {
        var result = await Task.Run(() =>
        {
            var context = new SystemContext();
            var userName = context.player_data.FirstOrDefault(r => r.mcid == minecraftId)?.uuid;
            context.Dispose();
            return userName ?? "";
        });
        
        return result;
    }
}