using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Man10UserServer.Models;

public class PlayerData
{
    [Key]
    public int id { get; set; }
    public string uuid { get; set; }
    public string mcid { get; set; }
    public DateTime? freeze_until { get; set; }
    public DateTime? mute_until { get; set; }
    public DateTime? jail_until { get; set; }
    public DateTime? ban_until { get; set; }
    public DateTime? msb_until { get; set; }
    public int score { get; set; }
}

public class ScoreLog
{
    [Key]
    public int id { get; set; }
    public string mcid { get; set; }
    public string uuid { get; set; }
    public string note { get; set; }
    public int score { get; set; }
    public string issuer { get; set; }
    public int now_score { get; set; }
    public DateTime? date { get; set; }
}

public class SystemContext : DbContext
{
    
    public DbSet<PlayerData> player_data { get; set; }
    public DbSet<ScoreLog> score_log { get; set; }
    private static string Host { get; set; }
    private static string Port { get; set; }
    private static string Pass { get; set; }
    private static string User { get; set; }
    private static string DatabaseName { get; set; }

    /// <summary>
    /// DBの接続設定を読み込む
    /// </summary>
    static SystemContext()
    {
        Host = "";
        Port = "";
        Pass = "";
        User = "";
        DatabaseName = "";

    }

    /// <summary>
    /// DBの接続を設定する
    /// </summary>
    /// <param name="config"></param>
    public static void SetDatabase(IConfiguration config)
    {
        Host = config["SystemDB:Host"] ?? "";
        Port = config["SystemDB:Port"] ?? "";
        Pass = config["SystemDB:Pass"] ?? "";
        User = config["SystemDB:User"] ?? "";
        DatabaseName = config["SystemDB:DatabaseName"] ?? "";

        var context = new SystemContext();
        Console.WriteLine(context.Database.CanConnect() ? "MySQLの接続を確認" : "MySQLの接続に失敗");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = $"server='{Host}';port='{Port}';user='{User}';password='{Pass}';Database='{DatabaseName}'";
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 30));
        optionsBuilder.UseMySql(connectionString, serverVersion);
    }
}