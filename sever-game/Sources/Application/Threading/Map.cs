using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.Interfaces.Character;
using TienKiemV2Remastered.Application.Interfaces.Map;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.DatabaseManager;
using TienKiemV2Remastered.Model.Character;
using TienKiemV2Remastered.Model.Item;
using TienKiemV2Remastered.Model.Map;
using static System.GC;
using TienKiemV2Remastered.Application.Map;
using TienKiemV2Remastered.Model.Monster;
using TienKiemV2Remastered.Model.Template;
using TienKiemV2Remastered.Model.Info;
using TienKiemV2Remastered.Model.Clan;
using TienKiemV2Remastered.Application.Extension.ChampionShip;

namespace TienKiemV2Remastered.Application.Threading
{
    public class Map
    {
        public int Id { get; set; }
        public List<Zone> Zones { get; set; }
        public TileMap TileMap { get; set; }
        public long TimeMap { get; set; }
        public bool IsRunning { get; set; }
        public bool IsStop { get; set; }
        public IMapCustom MapCustom { get; set; }
        public Task HandleZone { get; set; }
        public Zone zone { get; set; }

        public Map()
        {

        }

        public Map(int id, TileMap tileMap, IMapCustom mapCustom, bool isStart = true)
        {
            Id = id;
            Zones = new List<Zone>();
            TimeMap = -1;
            IsRunning = false;
            IsStop = false;
            MapCustom = mapCustom; 
            TileMap = tileMap ?? Cache.Gi().TILE_MAPS.FirstOrDefault(t => t.Id == Id);
            SetZone();
            if (isStart) Start();
            InitTrongTai();
        }

        public void Start()
        {
            if (IsRunning) return;
            IsRunning = true;
            StartHanleZone();
        }
        public void InitTrongTai()
        {
            switch (TileMap.Id)
            {
                case 52:
                    for (int i = 0; i < TileMap.ZoneNumbers; i++)
                    {
                        var trongtai = new Boss();
                        trongtai.CreateBoss(82, 372, 336);
                        trongtai.CharacterHandler.SetUpInfo();
                        Zones[i].ZoneHandler.AddBoss(trongtai);
                        ChampionShip.gI().TrongTai[0].Add(trongtai);
                    }
                    break;
                case 113:

                    for (int i = 0; i < TileMap.ZoneNumbers; i++)
                    {
                        var trongtai = new Boss();
                        trongtai.CreateBoss(82, 387, 264);
                        trongtai.CharacterHandler.SetUpInfo();
                        Zones[i].ZoneHandler.AddBoss(trongtai);
                        ChampionShip.gI().TrongTai[1].Add(trongtai);
                    }
                    break;
            }
        }


        private void StartHanleZone()
        {
            long t1, t2;
            async void Action()
            {
                while (IsRunning)
                {
                    
                        t1 = ServerUtils.CurrentTimeMillis();
                        Zones.ForEach(zone => zone.ZoneHandler.Update());
                        t2 = ServerUtils.CurrentTimeMillis() - t1;
                        await Task.Delay((int)Math.Abs(1000 - t2));
                    
                   
                }
                Zones.ForEach(zone => zone.ZoneHandler.Close());
                Zones.Clear();
                Zones = null;
                TileMap = null;
                HandleZone = null;
                SuppressFinalize(this);
            }
            HandleZone = new Task(Action);
            HandleZone.Start();
        }


        public void Close()
        {
            IsRunning = false;
        }

        public void SetZone()
        {
            for (var i = 0; i < TileMap.ZoneNumbers; i++)
            {
                Zones.Add(new Zone(i, this));
               
            }

            if (Id is not (21 or 22 or 23)) return;
            var item = ItemCache.GetItemDefault(74);//74
            short x = 0;
            short y = 0;
            switch (Id)
            {
                case 22:
                {
                    x = 55;
                    y = 325;
                    break;
                }
                case 21:
                case 23:
                {
                    x = 632;
                    y = 325;
                    break;
                }
            }
            Zones[0].ItemMaps.TryAdd(0, new ItemMap(-1)
            {
                Id = 0,
                PlayerId = -1,
                Item = item,
                X = x,
                Y = y
            });
        }

        //private async Task Update()
        //{
         ///   Parallel.ForEach(Zones, ZoneUpdate);
          //  await Task.Delay(10);
        //}

        //private async void ZoneUpdate(Zone zone)
        //{
        //    if (zone.Characters.Count > 0)
        //    {
        //        zone.ZoneHandler.Update();
        //    }
        //}

        public void JoinZone(Character character, int id, bool isDefault = false, bool isTeleport = false, int typeTeleport = 0)
        {
            
            if (id == -1)
            { 
                GetZoneNotMaxPlayer()?.ZoneHandler.JoinZone(character, isDefault, isTeleport, typeTeleport);
            }
            else
            {
                GetZoneById(id)?.ZoneHandler.JoinZone(character, isDefault, isTeleport, typeTeleport);
            }
        }
        public void JoinRandomZone(Character character, int id, bool isDefault = false, bool isTeleport = false, int typeTeleport = 0)
        {
            if (GetZoneById(id).Characters.Count >= 15)
            {
                GetZoneNotMaxPlayer().ZoneHandler.JoinZone(character, isDefault, isTeleport, typeTeleport);
            }
            else
            {

                GetZoneById(id)?.ZoneHandler.JoinZone(character, isDefault, isTeleport, typeTeleport);
            }
            
        }
       
        public Zone GetZoneNotMaxPlayer()
        {
            return Zones.FirstOrDefault(x => x.Characters.Count < TileMap.MaxPlayers);
        }

        public Zone GetZonePlayer()
        {
            return Zones.FirstOrDefault(x => x.Characters.Count >= 1);//4
        }
        public Zone GetZoneNotBoss()
        {
            return Zones.FirstOrDefault(x => x.Bosses.Count == 0);
        }
        public Zone MobAlive()
        {
            return Zones.FirstOrDefault(x => x.MonsterMaps.Count >= 0);//4
        }
        public Zone MobDieAll()
        {
            return Zones.FirstOrDefault(x => x.MonsterMaps.Count ==0);//4
        }
      
        public Character GetChar(int id)
        {
            var zonn = Zones.FirstOrDefault(x => x.Id == id);
            return zonn == null ? null : zonn.Characters.FirstOrDefault(c => c.Key > 0).Value;
        }

        public Zone GetZoneNotMaxPlayer(int id)
        {
            return Zones.FirstOrDefault(x => x.Id == id && x.Characters.Count < TileMap.MaxPlayers);
        }
        
        public Zone GetZoneById(int id)
        {
            return Zones.FirstOrDefault(x => x.Id == id);
        }
        public Zone GetZoneRandomNotHasPlayer()
        {
            return Zones.FirstOrDefault(x => x.Characters.Count == 0);
        }
        public void OutPrivateZone(Character character, int mapNextId)
        {
            switch (character.InfoChar.MapId)
            {
                case 45:
                case 46:
                case 47:
                case 48:
                case 50:
                case 154:
                    character.MapPrivate.GetMapById(character.InfoChar.MapId).OutZone(character, mapNextId);
                    break;
            }
        }
        public void OutSpecialZone(ICharacter character, int mapNextId)
        {
            if (!DataCache.IdMapSpecial.Contains(character.InfoChar.MapId) || ClanManager.Get(character.ClanId)==null)return;
            var clan = ClanManager.Get(character.ClanId);
          //  if (clan.Reddot.ReddotMaps[clan.Reddot.GetIndexMap(character.InfoChar.MapId)] == null || clan.Gas.GasMaps[clan.Gas.GetIndexMap(character.InfoChar.MapId)]==null) return; 
            switch (character.InfoChar.MapId)
            {
                case 53:
                    clan.Reddot.ReddotMaps[0].OutZone(character, mapNextId);
                    break;
                case 58:
                    clan.Reddot.ReddotMaps[1].OutZone(character, mapNextId);
                    break;
                case 59:
                    clan.Reddot.ReddotMaps[2].OutZone(character, mapNextId);
                    break;
                case 60:
                    clan.Reddot.ReddotMaps[3].OutZone(character, mapNextId);
                    break;
                case 61:
                    clan.Reddot.ReddotMaps[4].OutZone(character, mapNextId);
                    break;
                case 62:
                    clan.Reddot.ReddotMaps[5].OutZone(character, mapNextId);
                    break;
                case 55:
                    clan.Reddot.ReddotMaps[6].OutZone(character, mapNextId);
                    break;
                case 56:
                    clan.Reddot.ReddotMaps[7].OutZone(character, mapNextId);
                    break;
                case 54:
                    clan.Reddot.ReddotMaps[8].OutZone(character, mapNextId);
                    break;
                case 57:
                    clan.Reddot.ReddotMaps[9].OutZone(character, mapNextId);
                    break;
                case 141:
                case 142:
                case 143:
                    clan.cdrd.OutZone((Character)character, character.InfoChar.MapId, mapNextId);
                    break;
                case 147:
                case 148:
                case 149:
                case 151:
                case 152:
                    clan.Gas.OutZone((Character)character, character.InfoChar.MapId, mapNextId);
                    break;
                case 135:
                case 136:
                case 137:
                case 138:
                    clan.bdkb.OutZone((Character)character, character.InfoChar.MapId, mapNextId);
                    break;
            }
        }
        public void OutZone(ICharacter character, int mapNextId)
        {
            var isOutZone = mapNextId != Id && MapCustom?.GetMapById(mapNextId) == null && DataCache.IdMapCustom.Contains(Id);
            Zones.FirstOrDefault(x => x.Id == character.InfoChar.ZoneId)?.ZoneHandler?.OutZone(character, isOutZone);
        }
        
        public bool IsMapCustom()
        {
            return MapCustom != null || DataCache.IdMapCustom.Contains(Id);
        }
    }
}