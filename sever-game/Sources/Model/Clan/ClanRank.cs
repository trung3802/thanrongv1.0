using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.DatabaseManager;

namespace TienKiemV2Remastered.Model.Clan
{
    public class ClanRank
    {
        public class KhiGas
        {
            public string NameClan { get; set; }
            public int Rank { get; set; }
            public int Level { get; set; }
            public string LeaderName { get; set; }
            public List<short> Leader { get; set; }
            public List<long> HighScore { get; set; }
            // [0] score || [1] time set score

        }
        public static List<KhiGas> TopKhiGa = new List<KhiGas>();
        public static Message ListTopRankKhiGas()
        {
            var message = new Message(-96);
            message.Writer.WriteByte(0);
            message.Writer.WriteUTF("Top 100");
            message.Writer.WriteByte(TopKhiGa.Count < 100 ? TopKhiGa.Count : 100) ;
            TopKhiGa.ForEach(i =>
            {
            message.Writer.WriteInt(i.Rank); // rank
                message.Writer.WriteInt(-1); // pl id
                message.Writer.WriteShort(-1); // head id
                message.Writer.WriteShort(i.Leader[0]); // head icon
                message.Writer.WriteShort(i.Leader[1]); // body
                message.Writer.WriteShort(i.Leader[2]); // leg
                message.Writer.WriteUTF(i.NameClan);  // name
                message.Writer.WriteUTF($"Lv: {i.Level} ({ServerUtils.GetTimeInPast(ServerUtils.CurrentTimeMillis(), i.HighScore[1])})"); // name
                message.Writer.WriteUTF($"Bang chủ: {i.LeaderName}\n[{ServerUtils.GetTime(i.HighScore[0])}]"); // info 3
            });
            return message;
            
        }
        public static void SelectTopKhiGas(int limit)
        {
            TopKhiGa.Clear();
            lock (Server.SQLLOCK)
            {
                try
                {
                    DbContext.gI()?.ConnectToAccount();
                    using DbCommand command = DbContext.gI()?.Connection.CreateCommand();
                    if (command == null) return;
                    command.CommandText = $"SELECT `Name`, `LeaderName`,JSON_EXTRACT(Leader, '$.Head'), JSON_EXTRACT(Leader, '$.Body'), JSON_EXTRACT(Leader, '$.Leg'), JSON_EXTRACT(KhiGas, '$.HighScore') AS HighScore, JSON_EXTRACT(KhiGas, '$.TimeSetHighScore') AS TimeSetHighScore, JSON_EXTRACT(KhiGas, '$.LevelScore') FROM `clan` WHERE (JSON_EXTRACT(KhiGas, '$.LevelScore') > 0) ORDER BY HighScore,JSON_EXTRACT(KhiGas, '$.LevelScore'),TimeSetHighScore DESC LIMIT 100;";
                    using var reader = command.ExecuteReader();
                    if (!reader.HasRows) return;
                    int i = 1;
                    while (reader.Read())
                    {
                        TopKhiGa.Add(new KhiGas()
                        {
                            NameClan = reader.GetString(0),
                            Rank = i,
                            LeaderName = reader.GetString(1),
                            Leader = new List<short> { reader.GetInt16(2), reader.GetInt16(3), reader.GetInt16(4)},
                            HighScore = new List<long> { reader.GetInt64(5), reader.GetInt64(6)},
                            Level = reader.GetInt32(7)
                        }) ;
                        
                        i++;
                    }
                }
                catch (Exception e)
                {
                    Server.Gi().Logger.Error($"Error bxh {e.Message}\n{e.StackTrace}");
                }
                finally
                {
                    DbContext.gI()?.CloseConnect();
                }
            }
        }
    }
}
