using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Model.Character;

namespace TienKiemV2Remastered.Model.Clan
{
    public class ClanBoss
    {
        public int Level { get; set; }
        public int Count { get; set; }
        public long Time { get; set; }
        public bool Open { get; set; }
        public long TimeRefesh { get; set; }
        public bool RefeshMap { get; set; }
        public bool Close { get; set; }
        public void Start(Clan clan)
        {
            var boss = new Boss();
            boss.CreateBoss(95 + Level, 675, 552);
            boss.CharacterHandler.SetUpInfo();
            clan.ClanZone.Maps[1].Zones[0].ZoneHandler.AddBoss(boss);
            var timeServer = ServerUtils.CurrentTimeMillis();
            Time = timeServer + 1800000;
            TimeRefesh =  timeServer + DataCache._1DAY;
            Open = true;
            RefeshMap = false;
            Count++;
         
        }
        

    }
   
}
