
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
    public class Gas
    {
        public IList<Application.Threading.Map> GasMaps { get; set; }

        public long timeKhiGas { get; set; }
        public int Count { get; set; }
        public bool Open { get; set; }
        public int Level { get; set; }
         public long lastTimeMineCount {get;set;}
      public long HighScore {get;set;}
      public long TimeSetHighScore{get;set;}
      public long TimeGoBackLangAru {get;set;}
      public bool isFinish {get;set;}
        public int LevelScore { get; set; }
        public Gas()
        {
            timeKhiGas = ServerUtils.CurrentTimeMillis();
            Count = 1;
            Open = false;
            GasMaps = new List<Application.Threading.Map>();
            Level = 0;
            lastTimeMineCount = -1;
            HighScore = -1;
            LevelScore = -1;
            TimeSetHighScore = -1;
            TimeGoBackLangAru = -1;
            isFinish = false;
        }
        public void Clear()
        {
            Open = false;
            for (int i = 0; i < GasMaps.Count; i++)
            {
                GasMaps[i].Close();
                System.GC.SuppressFinalize(this.GasMaps);
                GasMaps[i] = null;
            }
        }
        public void OutZone(Character.Character character, int mapOldId, int mapNextId)
        {
            if (mapOldId == 149) GasMaps[0].OutZone(character, mapNextId);
            if (mapOldId == 147) GasMaps[1].OutZone(character, mapNextId);
            if (mapOldId == 152) GasMaps[2].OutZone(character, mapNextId);
            if (mapOldId == 151) GasMaps[3].OutZone(character, mapNextId);
            if (mapOldId == 148) GasMaps[4].OutZone(character, mapNextId);
        }
        public int GetIndexMap(int mapid)
        {
            switch (mapid)
            {
                case 149:
                    return 0;
                case 147:
                    return 1;
                case 152:
                    return 2;
                case 151:
                    return 3;
                case 148:
                    return 4;
            }
            return -1;
        }
        public void JoinZone(Character.Character character, int index)
        {

            var mapOld = MapManager.Get(character.InfoChar.MapId);
            if (DataCache.IdMapSpecial.Contains(mapOld.Id) && GasMaps[GetIndexMap(mapOld.Id)].Zones[0].ZoneHandler.GetCountMob() > 0)
            {
                character.InfoChar.X = (short)(character.InfoChar.X - 40);
                character.CharacterHandler.SendZoneMessage(Service.SendPos(character, 1));
                character.CharacterHandler.SendMessage(Service.DialogMessage("Đánh hết quái đi đã !"));
                return;
            }
            if (DataCache.IdMapSpecial.Contains(mapOld.Id)) mapOld.OutSpecialZone(character, GasMaps[index].Id);
            else mapOld.OutZone(character, GasMaps[index].Id);
            character.CharacterHandler.SetUpPosition(mapOld.Id, GasMaps[index].Id);
            GasMaps[index].JoinZone(character, 0);
        }
        //public void JoinZone(Character.Character character,int mapOldId,int mapNextId)
        //{
        //    var clan = ClanManagerr.Get(character.ClanId);
        //    switch (mapNextId)
        //    {
        //        case 149:
        //            OutZone(character, mapOldId, mapNextId);
        //            character.CharacterHandler.SetUpPosition(mapNextId, mapOldId);
        //            clan.Gas.GasMaps[0].JoinZone(character, 0);
        //            break;
        //        case 147:
        //            OutZone(character, mapOldId, mapNextId);
        //            character.CharacterHandler.SetUpPosition(mapNextId, mapOldId);
        //            clan.Gas.GasMaps[1].JoinZone(character, 0);
        //            break;
        //        case 152:
        //            OutZone(character, mapOldId, mapNextId);
        //            character.CharacterHandler.SetUpPosition(mapNextId, mapOldId);
        //            clan.Gas.GasMaps[2].JoinZone(character, 0);
        //            break;
        //        case 151:
        //            OutZone(character, mapOldId, mapNextId);
        //            character.CharacterHandler.SetUpPosition(mapNextId, mapOldId);
        //            clan.Gas.GasMaps[3].JoinZone(character, 0);
        //            break;
        //        case 148:
        //            OutZone(character, mapOldId, mapNextId);
        //            character.CharacterHandler.SetUpPosition(mapNextId, mapOldId);
        //            clan.Gas.GasMaps[4].JoinZone(character, 0);
        //            break;
        //    }
        //}
        public void Close(Character.Character character)
        {
            if (DataCache.IdMapGas.Contains(character.Zone.Map.Id))
            {
                var home = new Home(character.InfoChar.Gender);
                var mapOld = MapManager.Get(character.InfoChar.MapId);
                mapOld.OutZone(character, 0);
                MapManager.JoinMap(character, 0, ServerUtils.RandomNumber(1, 19), false, false, 0);
                character.CharacterHandler.SendMessage(Service.ServerMessage("Phó Bản Khí Ga đã kết thúc.Bạn đã được đưa về nhà !"));
            }
           
        }
        public void initMapKhiGas()
        {
            GasMaps.Clear();
            Open = true;
            Count -= 1;
            timeKhiGas = 1800000 + ServerUtils.CurrentTimeMillis();
            GasMaps.Add(new Application.Threading.Map(149, tileMap: null, mapCustom: null)); // 0

            GasMaps.Add(new Application.Threading.Map(147, tileMap: null, mapCustom: null)); // 1

            GasMaps.Add(new Application.Threading.Map(152, tileMap: null, mapCustom: null)); // 2

            GasMaps.Add(new Application.Threading.Map(151, tileMap: null, mapCustom: null)); // 3

            GasMaps.Add(new Application.Threading.Map(148, tileMap: null, mapCustom: null)); // 4
            Server.Gi().Logger.DebugColor("GasMaps Init COUNT: [" + GasMaps.Count + "]", "red");
        }
        public void InitDrLyche(int level)
        {
            var boss = new Boss();
            boss.CreateBossSetHp(23, level: Level);
            boss.CharacterHandler.SetUpInfo();
            GasMaps[4].Zones[0].ZoneHandler.AddBoss(boss);
        }
        public void InitHachijack(int level)
        {
            var boss = new Boss();
            boss.CreateBossSetHp(67, level: Level);
            boss.CharacterHandler.SetUpInfo();
            GasMaps[4].Zones[0].ZoneHandler.AddBoss(boss);
        }
        public void InitMob(int level)
        {
            Level = level;
            for (int gasmap = 0; gasmap < GasMaps.Count; gasmap++)
            {
                for (int mob = 0; mob < GasMaps[gasmap].Zones[0].MonsterMaps.Count; mob++)
                {
                    var monster = GasMaps[gasmap].Zones[0].MonsterMaps[mob];
                    monster.OriginalHp = monster.HpMax * 20 * level;
                    monster.MonsterHandler.SetUpMonster();
                }
            }
        }
    }
}