using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.Model.Character;
using TienKiemV2Remastered.Application.Extension.Bosses.Mabu12Gio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TienKiemV2Remastered.Application.Extension.Bosses.Mabu2Gio
{
    public class Mabu2h
    {
        public Boolean InitMabu2h = false;
        public long DelayInit = 15000 + ServerUtils.CurrentTimeMillis();
        public int CurrentTimeInit = 16;
        public static Mabu2h instance;
        public static Mabu2h gI()
        {
            if (instance == null)
            {
                instance = new Mabu2h();
            }
            return instance;
        }
        public void Update()
        {

        }
        public void Join(Character character)
        {
            var mapOld = MapManager.Get(character.InfoChar.MapId);
            mapOld.OutZone(character, 127);
            MapManager.JoinMap(character, 127, ServerUtils.RandomNumber(20), false, false, 0);
        }
        public void AutoInit(long timeserver)
        {
           if (timeserver >= DelayInit && ServerUtils.TimeNow().Hour == CurrentTimeInit)
           {
                DelayInit = timeserver + 3640000;
                Init();
           }         
        }
        public void Init()
        {
            Application.Threading.Map mapInit;
            mapInit = MapManager.Get(127);
            for (int i = 0; i < 20; i++)
            {
                var zoneInit = mapInit.GetZoneById(i);
                var mabu = new Boss();
                mabu.CreateBoss(43);
                mabu.CharacterHandler.SetUpInfo();
                zoneInit.ZoneHandler.AddBoss(mabu);

            }
            InitMabu2h = true;
            if (InitMabu2h)
            {
                Server.Gi().Logger.PrintColor("OPEN HOAT DONG MABU 2H", "cyan");
                ClientManager.Gi().SendMessageCharacter(Service.ServerMessage("Hoạt động Mabư 2h đã xuất hiện,các cụt thủ mau tham gia !"));
            }
        }
    }
}
