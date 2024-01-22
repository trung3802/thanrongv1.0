using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.Model.Monster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TienKiemV2Remastered.Model.Item;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered.Application.Interfaces.Monster;
using TienKiemV2Remastered.Model.Option;

namespace TienKiemV2Remastered.Application.Extension.Bosses.BigBoss
{
    public class Hirudegarn
    {
        public int TimeInit = 22;
        public bool Init = false;
        public static Hirudegarn instance;
        public static Hirudegarn gI()
        {
            if (instance == null) instance = new Hirudegarn();
            return instance;
        }
        public void InitHirudegarn(long currentHour)
        {
            if (!Init && currentHour == TimeInit)
            {
                Init = true;
            }else if (Init)
            {
                Init = false;
            }

        }
    }
    public class ThiefBear
    {
        public List<int> MapsSpawn = new List<int> { 3, 27, 28, 28, 4 };
        public List<short> X = new List<short> { 1247, 881, 659, 989, 454 };
        public List<short> Y = new List<short> { 336, 336, 312, 264, 336 };
        public long timeDelay = 10000 + ServerUtils.CurrentTimeMillis();
        public byte Count = 0;
        public int CurrentMapsSpawn = -1;
        public int CountSpawn = 0;
        public int LimitSpawn = 5;
        public int CurrentZonesSpawn = -1;
        public static ThiefBear instance;
        public static ThiefBear gI()
        {
            if (instance == null)
            {
                instance = new ThiefBear();
            }
            return instance;
        }
        public Task Runtime { get; set; }
        public void Start()
        {
            Runtime.Start();
        }
        public void LeaveItem(int charId,IMonster monster)
        {
            
            for (int i =0; i < 5; i++)
            {
                var Item2 = ItemCache.GetItemDefault((short)(1066 + i), 20);

                var itemMap2 = new ItemMap(charId, Item2);
                itemMap2.X = (short)(monster.X - (ServerUtils.RandomNumber(-50, 50)));
                itemMap2.Y = monster.Y;
                monster.Zone.ZoneHandler.LeaveItemMap(itemMap2);
            }
            for (int i2 = 0; i2 < 10; i2++)
            {
                var Item = ItemCache.GetItemDefault(861);

                //Item.Options.Add(new OptionItem()
                //{
                //    Id = 31,
                //    Param = 0,
                //});
                //Item.Options.Add(new OptionItem()
                //{
                //    Id = 93,
                //    Param =ServerUtils.RandomNumber(3,7) ,
                //});
                var itemMap = new ItemMap(charId, Item);
                itemMap.X = (short)(monster.X + i2 * 10);
                itemMap.Y = monster.Y;
                monster.Zone.ZoneHandler.LeaveItemMap(itemMap);
            }
        }
        public int GetIndexMap(int mapid)
        {
            switch (mapid)
            {
                case 3:
                    return 0;
                case 27:
                    return 1;
                case 28:
                    return 2;
                case 4:
                    return 4;
            }
            return -1;
        }
        bool refeshAllGau = false;
        public void Refesh()
        {
            Server.Gi().Logger.Print("Refest", "red");
            if (!refeshAllGau)
            {
                refeshAllGau = true;
                for (int i = 0; i < MapsSpawn.Count; i++)
                {
                    var map = MapManager.Get(MapsSpawn[i]);
                    for (int zone = 0; zone < 20; zone++)
                    {
                        var Zone = map.Zones[zone];
                        for (int mob = 0; mob < Zone.MonsterMaps.Count; mob++)
                        {
                            var monster = Zone.MonsterMaps[mob];
                            var timeserver = ServerUtils.CurrentTimeMillis();
                            if (monster != null && monster.Id == 77  && monster.Zone.Map.Id != CurrentMapsSpawn && monster.Zone.Id != CurrentZonesSpawn && monster.TimeAttack > timeserver)
                            {
                                monster.IsDie = true;
                                monster.Hp = 0;
                                monster.IsRefresh = false;
                                monster.Status = 0;
                                if (Zone.Characters.Count > 0)
                                {
                                    monster.Zone.ZoneHandler.SendMessage(Service.MonsterDie(monster.IdMap));
                                }
                            }
                        }
                    }

                }
            }
        }
        public void Init(long timeServer)
        {
           
            if (timeServer >= timeDelay && LimitSpawn < CountSpawn)
            {
                CountSpawn++;
                //CurrentMapsSpawn.Clear();
                //CurrentZonesSpawn.Clear();
                if (Count >= 1)
                {
                    refeshAllGau = false;
                    Refesh();
                }
                Count++;
                timeDelay = 900000 + ServerUtils.CurrentTimeMillis();
                var randomIndex = MapsSpawn[ServerUtils.RandomNumber(MapsSpawn.Count)];
                var zoneInit = MapManager.Get(randomIndex);
                var randomZone = ServerUtils.RandomNumber(1, 20);
                ClientManager.Gi().SendMessageCharacter(Service.ServerChat("BOSS Gấu Tướng Cướp vừa xuất hiện tại " + zoneInit.TileMap.Name));
                Server.Gi().Logger.PrintColor("Spawn ThiefBear: [Mapid:" + randomIndex + ", zone: " + randomZone + "]", "green");
                CurrentMapsSpawn = randomIndex;
                CurrentZonesSpawn = randomZone;
                var monster = MapManager.Get(randomIndex).Zones[randomZone].MonsterMaps.FirstOrDefault(i=>i.Id == 77);
                var character = MapManager.Get(randomIndex).Zones[randomZone].ZoneHandler.CharacterInMap();
                if (monster != null)
                {
                    monster.IsDie = false;
                    monster.Status = 5;
                    monster.LvBoss = 200;
                    monster.IsBoss = true;
                    if (character.Count > 0)
                    {
                        monster.Zone.ZoneHandler.SendMessage(Service.MobLive(monster));
                    }
                    monster.Hp = monster.OriginalHp = 10000000;
                    monster.MonsterHandler.SetUpMonster();
                }
            }
        }
    }
}

