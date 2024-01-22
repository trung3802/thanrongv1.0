using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.DatabaseManager;
using TienKiemV2Remastered.DatabaseManager.Player;
using TienKiemV2Remastered.Model.BangXepHang;
using TienKiemV2Remastered.Model.Character;

namespace TienKiemV2Remastered.Application.Threading
{
    public class ABXH
    {
        public List<BangXepHang.TopPower> TopPowers { get; set; }
        public List<BangXepHang.TopNap> TopNaps{get;set;}
        public List<BangXepHang.TopTask> TopTasks {get;set;}
        public List<BangXepHang.TopClanCDRD> TopClanCDRDs {get;set;}
        public List<BangXepHang.TopClanKhiGas> TopClanKhiGas{get;set;}
        public List<BangXepHang.TopEvent> TopEvent { get; set; }
        public List<BangXepHang.TopQuayThuong> TopQuayThuong { get; set; }
        public List<BangXepHang.TopSanBoss> TopSanBoss { get; set; }
        public List<BangXepHang.TopDisciple> TopDisciples { get; set; }


        public Task RunTime { get; set; }


        public ABXH()
        {
            TopDisciples = new List<BangXepHang.TopDisciple>();
            TopPowers = new List<BangXepHang.TopPower>();
            TopNaps = new List<BangXepHang.TopNap>();
            TopTasks = new List<BangXepHang.TopTask>();
            TopClanCDRDs = new List<BangXepHang.TopClanCDRD>();
            TopClanKhiGas = new List<BangXepHang.TopClanKhiGas>();
            TopEvent = new List<BangXepHang.TopEvent>();
            TopQuayThuong = new List<BangXepHang.TopQuayThuong>();
            TopSanBoss = new List<BangXepHang.TopSanBoss>();
        }
        public void Start()
        {
            if (RunTime != null) return;
            RunTime = new Task(Action);
            RunTime.Start();
        }
        public void Flush()
        {
            TopPowers.Clear();
            TopNaps.Clear();
            TopClanCDRDs.Clear();
            TopClanKhiGas.Clear();
            TopTasks.Clear();
            TopEvent.Clear();
            TopQuayThuong.Clear();
            TopSanBoss.Clear();
            TopDisciples.Clear();
        }
        private async void Action()
        {
            while (Server.Gi().IsRunning)
            {
                Flush();
                UpdateBangXepHang();
                Server.Gi().Logger.Print("UPDATE Top Bang Xep Hang !", "yellow");
                await Task.Delay(300000);
            }
        }
        public static void UpdateBangXepHang(){
            BXHDatabase.SelectTopPower(100);
            BXHDatabase.SelectTopTask(100);
            BXHDatabase.SelectTopEvent(100);
            BXHDatabase.SelectTopQuayThuong(100);
            BXHDatabase.SelectTopSanBoss(100);
            BXHDatabase.SelectTopNapThe(100);
            BXHDatabase.SelectTopDisciple(100);
        }

        public Message ListTopInfoEvent()
        {
            var message = new Message(-96);
            message.Writer.WriteByte(0);
            message.Writer.WriteUTF("Top 100 Hoa Quả Sơn");
            message.Writer.WriteByte(TopEvent.Count);
            TopEvent.ForEach(i =>
            {
                message.Writer.WriteInt(i.Top); // rank
                message.Writer.WriteInt(i.PlId); // pl id
                message.Writer.WriteShort(i.Head); // head id
                message.Writer.WriteShort(-1); // head icon
                message.Writer.WriteShort(i.Body); // body
                message.Writer.WriteShort(i.Leg); // leg
                message.Writer.WriteUTF(i.Name);  // name   
                message.Writer.WriteUTF(ServerUtils.GetMoneys(i.Point)); // name
                message.Writer.WriteUTF(ServerUtils.GetMoneys(i.Point)); // info 3
            });
            return message;
        }
        public Message ListTopDisciple()
        {
            var message = new Message(-96);
            message.Writer.WriteByte(0);
            message.Writer.WriteUTF("Top 100 Thiên Kiêu");
            message.Writer.WriteByte(TopDisciples.Count);
            TopDisciples.ForEach(i =>
            {
                message.Writer.WriteInt(i.I); // rank
                message.Writer.WriteInt(i.Id); // pl id
                message.Writer.WriteShort(i.Head); // head id
                message.Writer.WriteShort(-1); // head icon
                message.Writer.WriteShort(i.Body); // body
                message.Writer.WriteShort(i.Leg); // leg
                message.Writer.WriteUTF("Sư phụ: "+i.MasterName);  // name   
                message.Writer.WriteUTF(ServerUtils.GetMoneys(i.Power)); // name
                message.Writer.WriteUTF(i.Name); // info 3
            });
            return message;
        }
        public Message ListTopQuayThuong()
        {
            var message = new Message(-96);
            message.Writer.WriteByte(0);
            message.Writer.WriteUTF("Top 100 Quay Thưởng");
            message.Writer.WriteByte(TopQuayThuong.Count);
            TopQuayThuong.ForEach(i =>
            {
                message.Writer.WriteInt(i.Top); // rank
                message.Writer.WriteInt(i.PlId); // pl id
                message.Writer.WriteShort(i.Head); // head id
                message.Writer.WriteShort(-1); // head icon
                message.Writer.WriteShort(i.Body); // body
                message.Writer.WriteShort(i.Leg); // leg
                message.Writer.WriteUTF(i.Name);  // name
                message.Writer.WriteUTF(ServerUtils.GetMoneys(i.Point)); // name
                message.Writer.WriteUTF(ServerUtils.GetMoneys(i.Point)); // info 3
            });
            return message;
        }
        public Message ListTopSanBoss()
        {
            var message = new Message(-96);
            message.Writer.WriteByte(0);
            message.Writer.WriteUTF("Top 100 Sát Thần");
            message.Writer.WriteByte(TopSanBoss.Count);
            TopSanBoss.ForEach(i =>
            {
                message.Writer.WriteInt(i.Top); // rank
                message.Writer.WriteInt(i.PlId); // pl id
                message.Writer.WriteShort(i.Head); // head id
                message.Writer.WriteShort(-1); // head icon
                message.Writer.WriteShort(i.Body); // body
                message.Writer.WriteShort(i.Leg); // leg
                message.Writer.WriteUTF(i.Name);  // name
                message.Writer.WriteUTF(ServerUtils.GetMoneys(i.Point)); // name
                message.Writer.WriteUTF(ServerUtils.GetMoneys(i.Point)); // info 3
            });
            return message;
        }
        public Message ListTopInfoPower(){
            var message = new Message(-96);
            message.Writer.WriteByte(0);
            message.Writer.WriteUTF("Phong Thần Bảng");
            message.Writer.WriteByte(TopPowers.Count);
            TopPowers.ForEach(i=>
            {
                message.Writer.WriteInt(i.I); // rank
                message.Writer.WriteInt(i.Id); // pl id
                message.Writer.WriteShort(i.Head); // head id
                message.Writer.WriteShort(-1); // head icon
                message.Writer.WriteShort(i.Body); // body
                message.Writer.WriteShort(i.Leg); // leg
                message.Writer.WriteUTF(i.Name);  // name
                message.Writer.WriteUTF(ServerUtils.GetMoneys(i.Power)); // name
                message.Writer.WriteUTF(ServerUtils.GetMoneys(i.Power) + "\nTiềm năng: " + ServerUtils.GetMoneys(i.TotalPotential)); // info 3
            });
            return message;
        }
        public Message ListTopInfoNapThe(){
            var message = new Message(-96);
            message.Writer.WriteByte(0);
            message.Writer.WriteUTF("Tỷ Phú Bảng");
            message.Writer.WriteByte(TopNaps.Count);
            TopNaps.ForEach(i=>
            {
                message.Writer.WriteInt(i.I);
                message.Writer.WriteInt(i.Id);
                message.Writer.WriteShort(i.Head);
                message.Writer.WriteShort(-1);
                message.Writer.WriteShort(i.Body);
                message.Writer.WriteShort(i.Leg);
                message.Writer.WriteUTF(i.Name);
                message.Writer.WriteUTF("Đã nạp: " + ServerUtils.GetMoneys(i.TongNap));
                message.Writer.WriteUTF("Đã nạp: " + ServerUtils.GetMoneys(i.TongNap));
            });
            return message;
        }
       public Message ListTopInfoTask(){
            var message = new Message(-96);
            message.Writer.WriteByte(0);
            message.Writer.WriteUTF("Thiên Bảng");
            message.Writer.WriteByte(TopTasks.Count);
            TopTasks.ForEach(i=>
            {
                
                message.Writer.WriteInt(i.I);
                message.Writer.WriteInt(i.Id);
                message.Writer.WriteShort(i.Head);
                message.Writer.WriteShort(-1);
                message.Writer.WriteShort(i.Body);
                message.Writer.WriteShort(i.Leg);
                message.Writer.WriteUTF(i.Name);
                message.Writer.WriteUTF("Nhiệm vụ thứ " +i.TaskId);
                message.Writer.WriteUTF("Nhiệm vụ thứ " +i.TaskId + "\nGiai đoạn " + i.Index + " [" + i.Count + "]");
            });
            return message;
        }
    }
}