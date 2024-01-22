using System.Collections.Concurrent;
using System.Collections.Generic;
using TienKiemV2Remastered.Application.Handlers.Character;
using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Model.Info;
using TienKiemV2Remastered.Model.Clan;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Application.Map;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Model.Character;

namespace TienKiemV2Remastered.Model.Clan
{
	public class Treasure
	{
	  public   IList<Application.Threading.Map> MapBDKB { get; set; }
	  public  long timeBDKB { get; set; }
      public  int Count { get; set; }
      public bool Open { get; set; }
      public int Level { get; set; }
      public long lastTimeMineCount {get;set;}
      public long HighScore {get;set;}
      public long TimeGoBackDaoKame {get;set;}
      public bool isFinish {get;set;}
      public long timeSetHighScore {get;set;}
     public Treasure()
        {
            timeBDKB = 1500000 + ServerUtils.CurrentTimeMillis();
            Count = 3;
            Open = false;
            Level = 0;
            MapBDKB = new List<Application.Threading.Map>();
            lastTimeMineCount = -1;
            HighScore = -1;
            TimeGoBackDaoKame = -1;
            isFinish = false;
            timeSetHighScore = -1;
        }

        public void Init(int level)
        {
            Open = true;
            timeBDKB = 1800000 + ServerUtils.CurrentTimeMillis();
            Level = level;
            Count -= 1;
            MapBDKB.Clear();
            MapBDKB.Add(new Application.Threading.Map(135, tileMap: null, mapCustom: null));
            MapBDKB.Add(new Application.Threading.Map(136, tileMap: null, mapCustom: null));
            MapBDKB.Add(new Application.Threading.Map(138, tileMap: null, mapCustom: null));
            MapBDKB.Add(new Application.Threading.Map(137, tileMap: null, mapCustom: null));
            InitMob(level);
            InitBoss(level);
        }
        public int GetIndexMap(int mapid)
        {
            switch (mapid)
            {
                case 135:
                    return 0;
                case 136:
                    return 1;
                case 137:
                    return 3;
                case 138:
                    return 2;
            }
            return -1;
        }
        public void OutZone(Character.Character character, int mapOldId, int mapNextId)
        {
            MapBDKB[GetIndexMap(mapOldId)].OutZone(character, mapNextId);
        }
        public void JoinZone(Character.Character character, int index)
        {
            var mapOld = MapManager.Get(character.InfoChar.MapId);
            if (DataCache.IdMapBDKB.Contains(mapOld.Id) && MapBDKB[GetIndexMap(mapOld.Id)].Zones[0].ZoneHandler.GetCountMob() > 0)
            {
                mapOld.OutSpecialZone(character, MapBDKB[index].Id);
                character.CharacterHandler.SetUpPosition(MapBDKB[index].Id, mapOld.Id);
                MapBDKB[GetIndexMap(character.InfoChar.MapId)].JoinZone(character, 0);
                character.CharacterHandler.SendMessage(Service.ServerMessage("Đánh hết quái đi đã !"));
                return;
            }
            if (DataCache.IdMapSpecial.Contains(mapOld.Id)) mapOld.OutSpecialZone(character, MapBDKB[index].Id);
            else mapOld.OutZone(character, MapBDKB[index].Id);
            character.CharacterHandler.SetUpPosition(mapOld.Id, MapBDKB[index].Id);
            MapBDKB[index].JoinZone(character, 0);
        }
        public void Clear()
        {
                            Open = false;

            for (int i = 0; i < MapBDKB.Count; i++)
            {
                MapBDKB[i].Close();
                System.GC.SuppressFinalize(this.MapBDKB);
                MapBDKB[i] = null;
            }
        }
        public void Close(Character.Character character)
        {
            if (DataCache.IdMapBDKB.Contains(character.Zone.Map.Id))
            {
                var home = new Home(character.InfoChar.Gender);
                var mapOld = MapManager.Get(character.InfoChar.MapId);
                mapOld.OutZone(character, 21 + character.InfoChar.Gender);
                home.Maps[0].JoinZone(character, 0, true, true, 2);
                character.CharacterHandler.SendMessage(Service.ServerMessage("Bản Đồ Kho Báu đã kết thúc !"));
            }
           
        }
        public void InitBoss(int level)
        {
            var boss = new Boss();
            boss.CreateBossSetHp(48, level: Level);
            boss.CharacterHandler.SetUpInfo();
            MapBDKB[3].Zones[0].ZoneHandler.AddBoss(boss);
        }
        public void InitMob(int level)
        {
            Level = level;
            for (int bdkbmap = 0; bdkbmap < MapBDKB.Count; bdkbmap++)
            {
                for (int mob = 0; mob < MapBDKB[bdkbmap].Zones[0].MonsterMaps.Count; mob++)
                {
                    var monster = MapBDKB[bdkbmap].Zones[0].MonsterMaps[mob];
                    if (monster.Id != 71 || monster.Id != 72)
                    {
                        monster.OriginalHp = monster.HpMax * 20 * level;
                    }
                    else monster.OriginalHp = monster.HpMax * level;
                    monster.MonsterHandler.SetUpMonster();
                }
            }
        }
    }
}