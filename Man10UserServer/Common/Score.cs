using System.Runtime.CompilerServices;
using Man10UserServer.Controllers;
using Man10UserServer.Models;

namespace Man10UserServer.Common;

public static class Score
{

    private static readonly SystemContext Context = new();
    
    /// <summary>
    /// スコアを取得
    /// </summary>
    /// <param name="uuid"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public static Task<int> Get(string uuid)
    {
        var result = Task.Run(() =>
        {
            var ret = Context.player_data.FirstOrDefault(r => r.uuid == uuid)?.score ?? 0;
            return ret; 
        });

        return result;
    }
    
    /// <summary>
    /// スコア追加
    /// 200:成功
    /// 550:ユーザーデータなし
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public static Task<int> Give(ScoreData data)
    {
        var result = Task.Run(() =>
        {
            var record = Context.player_data.FirstOrDefault(r => r.uuid == data.UUID);

            if (record == null)
            {
                return 550;
            }

            record.score += data.Amount;

            var log = new ScoreLog
            {
                uuid = data.UUID,
                score = data.Amount,
                now_score = record.score,
                date = DateTime.Now,
                note = $"[give]:{data.Note}",
                issuer = data.Issuer
            };

            Context.score_log.Add(log);
            Context.SaveChanges();

            return 200;
        });
            
        return result;
    }
    
    /// <summary>
    /// スコア削減
    /// 200:成功
    /// 550:ユーザーデータなし
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public static Task<int> Take(ScoreData data)
    {
        var result = Task.Run(() =>
        {
            var record = Context.player_data.FirstOrDefault(r => r.uuid == data.UUID);

            if (record == null)
            {
                return 550;
            }

            record.score -= data.Amount;

            var log = new ScoreLog
            {
                uuid = data.UUID,
                score = data.Amount,
                now_score = record.score,
                date = DateTime.Now,
                note = $"[take]:{data.Note}",
                issuer = data.Issuer
            };

            Context.score_log.Add(log);
            Context.SaveChanges();

            return 200;
        });
            
        return result;
    }
    
    
}