using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.Model.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TienKiemV2Remastered.Application.MainTasks;

namespace TienKiemV2Remastered.Application.Extension.Bosses.Mabu12Gio
{
    public class Mabu12h
    {
        public Boolean InitMabu12h = false;
        public long currTimeMabu12h = 13; // 12
        public long DelayInit = 11000 + ServerUtils.CurrentTimeMillis();
        public static Mabu12h instance;
        public Task RunTime { get; set; }
        public static Mabu12h gI()
        {
            if (instance == null)
            {
                instance = new Mabu12h();
            }
            return instance;
        }
        public void Update()
        {
            
        }
        
        public void AutoInit(long timeserver)
        {
        
                if (timeserver >= DelayInit  && Time())
                {
                    DelayInit = timeserver + 3600000;
                    Init();
                }
               
        }
        public Boolean Time()
        {
            if (ServerUtils.TimeNow().Hour == currTimeMabu12h)
                return true;
            return false;
        }   
        public void Join(Character character)
        {
            if (TaskHandler.CheckTask(character, 31, 0)) TaskHandler.gI().PlusSubTask(character, 1);
            MapManager.JoinMap(character, 114, ServerUtils.RandomNumber(20), false, false, 0);
            character.CharacterHandler.SendMessage(Mabu12hService.sendPowerInfo("TL", 0, 50, 10));
            character.PPower = 0;
            var Flag = ServerUtils.RandomNumber(9, 10);
            character.Flag = (sbyte)Flag;
            character.CharacterHandler.SendMessage(Service.ChangeFlag1(character.Id, Flag));
        }
        public void InitMabu(int zonee)
        {
            Application.Threading.Map mapInit;
            mapInit = MapManager.Get(120);
            var zone = mapInit.GetZoneById(zonee);
            var boss = new Boss();
            boss.CreateBoss(39);
            boss.CharacterHandler.SetUpInfo();
            zone.ZoneHandler.AddBoss(boss);
        }
        public void Init()
        {
            InitMabu12h = true;
            var ListMap = DataCache.IdMapMabu;
            var listBoss = new List<int> {36,37,37,38,36,36};
            for (int i = 0; i < ListMap.Count; i++)
            {
                Application.Threading.Map mapInit;
                mapInit = MapManager.Get(ListMap[i]);
                for (int z = 0; z < 20; z++)
                {
                    var zoneInit = mapInit.GetZoneById(z);
                    var boss = new Boss();
                    boss.CreateBoss(listBoss[i]);
                    boss.CharacterHandler.SetUpInfo();
                    zoneInit.ZoneHandler.AddBoss(boss);
                    Thread.Sleep(50);
                }
            }
            Server.Gi().Logger.PrintColor("OPEN HOAT DONG MABU 12H", "cyan");
            ClientManager.Gi().SendMessageCharacter(Service.ServerMessage("Hoạt động Mabư 12h đã xuất hiện,các cụt thủ mau tham gia !"));
        }
    }
}
