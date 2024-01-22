

using System.Collections.Concurrent;
using System.Collections.Generic;
using TienKiemV2Remastered.Application.Handlers.Character;
using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Model.Info;
using TienKiemV2Remastered.Model.Clan;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Application.Map;
using TienKiemV2Remastered.Model.Character;

namespace TienKiemV2Remastered.Model.Clan
{
    public class CDRC
    {
        public  IList<Application.Threading.Map> MapCDRD { get; set; }
        public  long timeCDRD { get; set; }
        public  int Count { get; set; }
        public bool Open { get; set; }
        public int Level { get; set; }
        public List<Boss> Bosses = new List<Boss>();
        public bool isFinish {get;set;}
        public CDRC()
        {
            timeCDRD = 1500000;
            Count = 3;
            Open = false;
            Level = 0;
            MapCDRD = new List<Application.Threading.Map>();
            isFinish = false;
        }
        public void Clear()
        {
            Open = false;
            for (int i = 0; i < MapCDRD.Count; i++)
            {
                MapCDRD[i].Close();
                System.GC.SuppressFinalize(this.MapCDRD);
                MapCDRD[i] = null;
            }
        }
        public int GetIndexMap(int mapid)
        {
            switch (mapid)
            {
                case 141:
                    return 2;
                case 142:
                    return 1;
                case 143:
                    return 0;
                case 144:
                    return 3;
            }
            return -1;
        }
        public void OutZone(Character.Character character, int mapOldId, int mapNextId)
        {
            MapCDRD[GetIndexMap(mapOldId)].OutZone(character, mapNextId);
            Server.Gi().Logger.Print("OUT MAP: " + mapOldId, "cyan");
        }
        public void JoinZone(Character.Character character, int index)
        {
            var mapOld = MapManager.Get(character.InfoChar.MapId);
            if (DataCache.IdMapCDRD.Contains(mapOld.Id) && MapCDRD[GetIndexMap(mapOld.Id)].Zones[0].ZoneHandler.GetCountMob() > 0)
            { 
                mapOld.OutSpecialZone(character, MapCDRD[index].Id);
                character.CharacterHandler.SetUpPosition(MapCDRD[index].Id, mapOld.Id);
                MapCDRD[GetIndexMap(character.InfoChar.MapId)].JoinZone(character, 0);
                character.CharacterHandler.SendMessage(Service.ServerMessage("Đánh hết quái đi đã !"));
                return;
            }
            if (DataCache.IdMapSpecial.Contains(mapOld.Id)) mapOld.OutSpecialZone(character, MapCDRD[index].Id);
            else mapOld.OutZone(character, MapCDRD[index].Id);
            character.CharacterHandler.SetUpPosition(mapOld.Id, MapCDRD[index].Id);
            MapCDRD[index].JoinZone(character, 0);
        }
        public void Close(Character.Character character)
        {
            Open = false;
            if (DataCache.IdMapCDRD.Contains(character.Zone.Map.Id))
            {
                var home = new Home(character.InfoChar.Gender);
                var mapOld = MapManager.Get(character.InfoChar.MapId);
                mapOld.OutZone(character, 21 + character.InfoChar.Gender);
                home.Maps[0].JoinZone(character, 0, true, true, 2);
                character.CharacterHandler.SendMessage(Service.ServerMessage("Con đường rắn độc đã kết thúc.Bạn được đưa về nhà !"));
            }
           
        }
      
        public void Init(int level)
        {
            MapCDRD.Clear();
            Open = true;
            Count -= 1;
            Level = level;
            timeCDRD = 1800000 + ServerUtils.CurrentTimeMillis();
            MapCDRD.Add(new Application.Threading.Map(143, tileMap: null, mapCustom: null));
            MapCDRD.Add(new Application.Threading.Map(142, tileMap: null, mapCustom: null));
            MapCDRD.Add(new Application.Threading.Map(141, tileMap: null, mapCustom: null));
            MapCDRD.Add(new Application.Threading.Map(144, tileMap: null, mapCustom: null));
            InitMob(level);
            InitBoss(level);
        }
        public void InitMob(int level)
        {
            for (int Map = 0; Map < MapCDRD.Count; Map++)
            {
                for (int mob = 0; mob < MapCDRD[Map].Zones[0].MonsterMaps.Count; mob++)
                {
                    var monster = MapCDRD[Map].Zones[0].MonsterMaps[mob];
                    monster.OriginalHp = monster.HpMax * 20 * level;
                    monster.MonsterHandler.SetUpMonster();
                }
            }
        }
        public void InitBoss(int level)
        {
            for (int i = 0; i < 5; i++)
            {
                //
                var saibamen = new Boss();
                if (i == 0) saibamen.CreateBossSetHp(73, level: level, typePk:5);
                else saibamen.CreateBossSetHp(73+i, level: level, typePk: 0);
                saibamen.CharacterHandler.SetUpInfo();
                MapCDRD[3].Zones[0].ZoneHandler.AddBoss(saibamen);
                Bosses.Add(saibamen);
            }
            //
            var nappa = new Boss();
            nappa.CreateBossSetHp(78, level: level, typePk: 0);
            nappa.CharacterHandler.SetUpInfo();
            MapCDRD[3].Zones[0].ZoneHandler.AddBoss(nappa);
            Bosses.Add(nappa);
            //
            var cadic = new Boss();
            cadic.CreateBossSetHp(79, level: level, typePk: 0);
            cadic.CharacterHandler.SetUpInfo();
            MapCDRD[3].Zones[0].ZoneHandler.AddBoss(cadic);
            Bosses.Add(cadic);
            for (int j = 0; j < Bosses.Count; j++)
            {
                Server.Gi().Logger.Print("Bossname: " + Bosses[j].Name);
            }
        }
    }
}