using System.Collections.Generic;
using System.Linq;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.DatabaseManager;
using TienKiemV2Remastered.Model.Character;

namespace TienKiemV2Remastered.Application.Extension.Super_Champion
{
    public class SieuHang
    {
        public class InfoRank
        {
            public int Top { get; set; }
            public int PlayerId { get; set; }
            public string PlayerName { get; set; }
            public short Head { get; set; }
            public short Body { get; set; }
            public short Leg { get; set; }
            public int Gem { get; set; }
            public History History { get; set; }
            public Point Point { get; set; }
        }
        public class History
        {
            public string[][] Text { get; set; }
            public History()
            {
                Text = new string[0][];
            }
        }
       
        public class Point
        {
            public long HP { get; set; }
            public long SD { get; set; }
            public long Amor { get; set; }
            public int Win { get; set; }
            public Point()
            {
                HP = -1;
                SD = -1;
                Amor = -1;
                Win = 0;
            }
            public Point(long hp, long sd, long amor, int win){
                HP = hp;
                SD = sd;
                amor = Amor;
                Win = win;
            }
        }
        public class InfoPlayer
        {
            public int Ticket { get; set; }
            public int Top { get; set; }
            public History History { get; set; }
            public int Gem { get; set; }
            public Point point { get; set; }
            public InfoPlayer()
            {
                Ticket = 3;
                Top = Cache.Gi().InfoRankSieuHang.Count;
                History = new History();
                Gem = 100;
                point = new Point();
            }
        }
        public class MySql
        {

        }
        public static void ThachDau(Character character, int playerThachDauId)
        {
            //var player = Cache.Gi().InfoRankSieuHang.FirstOrDefault(i=>i.Value.PlayerId == playerThachDauId);
            Server.Gi().Logger.Print("PlId: " + playerThachDauId, "cyan");
        }
        public static List<InfoRank> ListInfoRank(Character character)
        {
            List<InfoRank> Infos = new List<InfoRank>();

            return Infos;
        }
        public static Message ListRank(Character character)
        {
            var message = new Message(-96);
            message.Writer.WriteByte(0);
            message.Writer.WriteUTF("Top 100 Cao Thủ");
            message.Writer.WriteByte(10);
            for (int i = 0; i < 10; i++)
            {

                var time = ServerUtils.CurrentTimeMillis();
                var rank = Cache.Gi().InfoRankSieuHang[0];
                var info = "";
                if (i == 9)
                {
                    message.Writer.WriteInt(i + 1); // rank
                    message.Writer.WriteInt(character.Id); // pl id
                    message.Writer.WriteShort(character.GetHead()); // head id
                    message.Writer.WriteShort(-1); // head icon
                    message.Writer.WriteShort(character.GetBody()); // body
                    message.Writer.WriteShort(character.GetLeg()); // leg
                    message.Writer.WriteUTF(character.Name);  // name
                    message.Writer.WriteUTF(""); // phía bên ngoài | khi bật menu lên là chữ màu xanh lá
                    //if (rank.History.Info.Count >= 1 && rank.PlayerId != character.Id)
                    //{
                     //   for (int infoRank = 0; infoRank < rank.History.Info.Count; infoRank++)
                     //   {
                     //       info += rank.History.Info[infoRank].Text + $"{(time - rank.History.Info[infoRank].currentTime > 1 ? "" : "" + ServerUtils.GetTime(time - rank.History.Info[infoRank].currentTime))}" + (rank.History.Info.Count == 1 ? "" : "\n");
                     //   }
                    //}
                    //if (rank.PlayerId == character.Id && character.DataSieuHang.History.Info.Count >= 1)
                    //{
                        for (int history = 0; history < character.DataSieuHang.History.Text.Length; history++)
                        {
                            info += character.DataSieuHang.History.Text[history][0] + (time - long.Parse(character.DataSieuHang.History.Text[history][1]) > 1 ? "" : $" {ServerUtils.GetTime(time - long.Parse(character.DataSieuHang.History.Text[history][1]))}") + (character.DataSieuHang.History.Text.Length == 1 ? "" : "\n");
                        }
                    //}
                    message.Writer.WriteUTF(
                    $"HP {character.DataSieuHang.point.HP}\nSức đánh {character.DataSieuHang.point.SD}\nGiáp {character.DataSieuHang.point.Amor}\n{character.DataSieuHang.point.Win}\n"
                    + $"{info}"); // khi bật menu lên là chữ màu xanh dương
                }
                else
                {
                    message.Writer.WriteInt(i + 1); // rank
                    message.Writer.WriteInt(rank.PlayerId); // pl id
                    message.Writer.WriteShort(rank.Head); // head id
                    message.Writer.WriteShort(-1); // head icon
                    message.Writer.WriteShort(rank.Body); // body
                    message.Writer.WriteShort(rank.Leg); // leg
                    message.Writer.WriteUTF(rank.PlayerName);  // name
                    message.Writer.WriteUTF(""); // phía bên ngoài | khi bật menu lên là chữ màu xanh lá
                    if (rank.History.Text.Length >= 1 && rank.PlayerId != character.Id)
                    {
                        for (int infoRank = 0; infoRank < rank.History.Text.Length; infoRank++)
                        {
                            info += rank.History.Text[infoRank][0] + $"{(time - long.Parse(rank.History.Text[infoRank][1]) > 1 ? "" : "" + ServerUtils.GetTime(time - long.Parse(rank.History.Text[infoRank][1])))}" + (rank.History.Text.Length == 1 ? "" : "\n");
                        }
                    }
                    if (rank.PlayerId == character.Id && character.DataSieuHang.History.Text.Length >= 1)
                    {
                        for (int history = 0; history < character.DataSieuHang.History.Text.Length; history++)
                        {
                            info += character.DataSieuHang.History.Text[history][0] + (time - long.Parse(character.DataSieuHang.History.Text[history][1]) > 1 ? "" : $" {ServerUtils.GetTime(time - long.Parse(character.DataSieuHang.History.Text[history][1]))}") + (character.DataSieuHang.History.Text.Length == 1 ? "" : "\n");
                        }
                    }
                    message.Writer.WriteUTF((rank.PlayerId == character.Id ?
                    $"HP {character.DataSieuHang.point.HP}\nSức đánh {character.DataSieuHang.point.SD}\nGiáp {character.DataSieuHang.point.Amor}\n{character.DataSieuHang.point.Win}\n"
                    : $"HP {rank.Point.HP}\nSức đánh {rank.Point.SD}\nGiáp {rank.Point.Amor}\n{rank.Point.Win}")
                    + $"{info}"); // khi bật menu lên là chữ màu xanh dương
                }
            }
            //character.CharacterHandler.SendMessage(message);
            return message;
        }
    }
}