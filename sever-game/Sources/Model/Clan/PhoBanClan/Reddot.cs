
using System.Collections.Concurrent;
using System.Collections.Generic;
using TienKiemV2Remastered.Application.Handlers.Character;
using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Model.Info;
using TienKiemV2Remastered.Model.Clan;
using TienKiemV2Remastered.Application.Threading;
using System.Linq;
using System;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.Map;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Application.Main;
using System.Threading.Tasks;
using TienKiemV2Remastered.Model.Item;
using TienKiemV2Remastered.Model.Character;
namespace TienKiemV2Remastered.Model.Clan
{
    public class Reddot
	{
        public IList<Application.Threading.Map> ReddotMaps { get; set; }
      public  long timeDoanhTrai { get; set; }
      public  int Count { get; set; }
        public bool Open { get; set; }
        public bool isFinish {get;set;}
      public Reddot()
        {
            timeDoanhTrai = 1500000 + ServerUtils.CurrentTimeMillis();
            Count = 2;
            Open = false;
            ReddotMaps = new List<Application.Threading.Map>();
            isFinish = false;
            //InitReddot();
        }
       
        public void Clear()
        {
            Open = false;
            Count -= 1;
            for (int i = 0; i < ReddotMaps.Count; i++)
            {
                if (ReddotMaps[i].Zones[0].Characters.Count <= 0)
                {
                    ReddotMaps[i].Close();
                    System.GC.SuppressFinalize(this.ReddotMaps);
                    ReddotMaps[i] = null;
                }
                
            }
        }
        public void Close(Character.Character character)
        {
            if (DataCache.IdMapReddot.Contains(character.Zone.Map.Id))
            {
                var home = new Home(character.InfoChar.Gender);
                var mapOld = MapManager.Get(character.InfoChar.MapId);
                mapOld.OutZone(character, 21 + character.InfoChar.Gender);
                home.Maps[0].JoinZone(character, 0, true, true, 2);
                character.CharacterHandler.SendMessage(Service.ServerMessage("Doanh trại đã kết thúc.Bạn đã được đưa về nhà !"));
            }
          
        }
        public void OutZone(Character.Character character, int mapNewId)
        {
            var map = character.InfoChar.MapId; 
            if (map == 53) ReddotMaps[0].OutZone(character, mapNewId);
            else if (map == 58) ReddotMaps[1].OutZone(character, mapNewId);
            else if (map == 59) ReddotMaps[2].OutZone(character, mapNewId);
            else if (map == 60) ReddotMaps[3].OutZone(character, mapNewId);
            else if (map == 61) ReddotMaps[4].OutZone(character, mapNewId);
            else if (map == 62) ReddotMaps[5].OutZone(character, mapNewId);
            else if (map == 55) ReddotMaps[6].OutZone(character, mapNewId);
            else if (map == 56) ReddotMaps[7].OutZone(character, mapNewId);
            else if (map == 54) ReddotMaps[8].OutZone(character, mapNewId);
            else ReddotMaps[9].OutZone(character, mapNewId);            
        }
        public int GetIndexMap(int mapid)
        {
            switch (mapid)
            {
                case 53:
                    return 0;
                case 58:
                    return 1;
                case 59:
                    return 2;
                case 60:
                    return 3;
                case 61:
                    return 4;
                case 62:
                    return 5;
                case 55:
                    return 6;
                case 56:
                    return 7;
                case 54:
                    return 8;
                case 57:
                    return 9;
             }
            Server.Gi().Logger.Print("mapid err:  " + mapid, "red");
            return -1;
            
        }
        public void JoinZone(Character.Character character, int index)
        {
            var mapOld = MapManager.Get(character.InfoChar.MapId);
            if (DataCache.IdMapSpecial.Contains(mapOld.Id) && ReddotMaps[GetIndexMap(mapOld.Id)].Zones[0].ZoneHandler.GetCountMob() > 0)
            {
                character.InfoChar.X = (short)(character.InfoChar.X - 40);
                character.CharacterHandler.SendZoneMessage(Service.SendPos(character, 1));
                character.CharacterHandler.SendMessage(Service.DialogMessage("Đánh hết quái đi đã !"));
                return;
            }
            if (DataCache.IdMapSpecial.Contains(mapOld.Id)) mapOld.OutSpecialZone(character, ReddotMaps[index].Id);
            else mapOld.OutZone(character, ReddotMaps[index].Id);
            character.CharacterHandler.SetUpPosition(mapOld.Id, ReddotMaps[index].Id);
            ReddotMaps[index].JoinZone(character, 0);

        }
        public ItemMap drop()
        {
            var item = ItemCache.GetItemDefault((short)ServerUtils.RandomNumber(17,20));
            item.Quantity = 1;
            return new ItemMap(-1, item);
        }

        public void Win()
        {
            timeDoanhTrai = 300000 + ServerUtils.CurrentTimeMillis();
            
                Server.Gi().Logger.PrintColor("SUMMON DRAGON BALL IN REDDOT","red");
                TienKiemV2Remastered.Application.Threading.Map mapInit;
                for (int i = 0; i < ReddotMaps.Count; i++)
                {
                    var itemMap = drop();
                    List<int> listX = new List<int> {316,647,437 ,485,1148,613,655,711,577,1252};
                    List<int> listY = new List<int> { 384,336,384,288,216,384,240,312,312,312};
                    mapInit = ReddotMaps[i];
                    itemMap.X = (short)listX[i];
                    itemMap.Y = (short)listY[i];
                    mapInit.Zones[0].ZoneHandler.LeaveItemMap(itemMap);
                }
        }
        public void InitReddot()
        {
            Open = true;
            Count -= 1;
            timeDoanhTrai = 1800000 + ServerUtils.CurrentTimeMillis();
            ReddotMaps.Clear();
            // --- TUONG THANH
            ReddotMaps.Add(new Application.Threading.Map(53, tileMap: null, mapCustom: null));
            ReddotMaps.Add(new Application.Threading.Map(58, tileMap: null, mapCustom: null));
            ReddotMaps.Add(new Application.Threading.Map(59, tileMap: null, mapCustom: null));
            // --- TRAI DOC NHAN
            ReddotMaps.Add(new Application.Threading.Map(60, tileMap: null, mapCustom: null));
            ReddotMaps.Add(new Application.Threading.Map(61, tileMap: null, mapCustom: null));
            ReddotMaps.Add(new Application.Threading.Map(62, tileMap: null, mapCustom: null));
            // --- TANG
            ReddotMaps.Add(new Application.Threading.Map(55, tileMap: null, mapCustom: null));
            ReddotMaps.Add(new Application.Threading.Map(56, tileMap: null, mapCustom: null));
            ReddotMaps.Add(new Application.Threading.Map(54, tileMap: null, mapCustom: null));
            ReddotMaps.Add(new Application.Threading.Map(57, tileMap: null, mapCustom: null));
            Server.Gi().Logger.DebugColor("ReddotMaps Init COUNT: [" + ReddotMaps.Count + "]", "red");
        }
        public void InitBoss(Character.Character character)
        {
            var percentHp = character.HpFull;
            var clan = ClanManager.Get(character.ClanId);
            clan.Thành_viên.ForEach(member =>
            {
                var imember = ClientManager.Gi().GetCharacter(member.Id);
                if (imember != null)
                {
                    percentHp += imember.HpFull;
                }
            });
            if (percentHp > 500000)
            {
                percentHp = 500000;
            }
            Boss _TUT = new Boss();
            _TUT.CreateBossDoanhTrai(47, 923, 384, (int)percentHp);
            _TUT.CharacterHandler.SetUpInfo();
            ReddotMaps[2].Zones[0].ZoneHandler.AddBoss(_TUT);
            ///
            Boss _TUXL = new Boss();
            _TUXL.CreateBossDoanhTrai(48, 1088, 384, (int)percentHp);
            _TUXL.CharacterHandler.SetUpInfo();
            ReddotMaps[5].Zones[0].ZoneHandler.AddBoss(_TUXL);
            ///
            Boss _TUTHEP = new Boss();
            _TUTHEP.CreateBossDoanhTrai(49, 830, 312, (int)percentHp);
            _TUTHEP.CharacterHandler.SetUpInfo();
            ReddotMaps[6].Zones[0].ZoneHandler.AddBoss(_TUTHEP);
            ///
            Boss _NJAOTIM = new Boss();
            _NJAOTIM.CreateBossDoanhTrai(50, 994, 312, (int)percentHp);
            _NJAOTIM.CharacterHandler.SetUpInfo();
            ReddotMaps[8].Zones[0].ZoneHandler.AddBoss(_NJAOTIM);
            //// --- VE SI 1
            Boss _VESI = new Boss();
            _VESI.CreateBossDoanhTrai(51, 1443, 312, (int)percentHp);
            _VESI.CharacterHandler.SetUpInfo();
            ReddotMaps[9].Zones[0].ZoneHandler.AddBoss(_VESI);
            /// --- VE SI 2
            Boss _VESI2 = new Boss();
            _VESI2.CreateBossDoanhTrai(51, 1493, 312, (int)percentHp);
            _VESI2.CharacterHandler.SetUpInfo();
            ReddotMaps[9].Zones[0].ZoneHandler.AddBoss(_VESI2);
            /// --- VE SI 3
            Boss _VESI3 = new Boss();
            _VESI3.CreateBossDoanhTrai(51, 1393, 312, (int)percentHp);
            _VESI3.CharacterHandler.SetUpInfo();
            ReddotMaps[9].Zones[0].ZoneHandler.AddBoss(_VESI3);
            /// --- VE SI 4
            Boss _VESI4 = new Boss();
            _VESI4.CreateBossDoanhTrai(51, 1343, 312, (int)percentHp);
            _VESI4.CharacterHandler.SetUpInfo();
            ReddotMaps[9].Zones[0].ZoneHandler.AddBoss(_VESI4);
        }
        public void InitMobHp(Character.Character character)
        {
            var dmgfull = character.DamageFull;
            for (int i = 0; i < ReddotMaps.Count; i++)
            {
                for (int mob = 0; mob < ReddotMaps[i].Zones[0].MonsterMaps.Count; mob++)
                {
                    var monster = ReddotMaps[i].Zones[0].MonsterMaps[mob];
                    
                        var clan = ClanManager.Get(character.ClanId);
                        var totalDmg = 0;
                        for (int i2 = 0; i2 < clan.Thành_viên.Count; i2++)
                        {
                            if (ClientManager.Gi().GetCharacter(clan.Thành_viên[i2].Id) != null)
                            {
                                var @memberInClan = ClientManager.Gi().GetCharacter(clan.Thành_viên[i2].Id);
                                totalDmg += memberInClan.DamageFull;
                            }
                        }
                        monster.OriginalHp += totalDmg*6;
                        monster.MonsterHandler.SetUpMonster();
                    
                }
            }
        }
	}
}