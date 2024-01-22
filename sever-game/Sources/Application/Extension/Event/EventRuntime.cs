using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.Model.Character;
using TienKiemV2Remastered.Application.Manager;

namespace TienKiemV2Remastered.Application.Extension.Event
{
    public class EventRuntime
    {
        public static readonly List<List<int>> DataTradeDuaHau = new List<List<int>> { new List<int> {30,25,20,10,1 }, new List<int> {200,150,100,30,2 } }; // dua hau, ngoc
        public readonly List<List<int>> DataHungVuong = new List<List<int>> { }; // map, zone, x, y
        public static long TimeCollectHatGiong = 300000 + ServerUtils.CurrentTimeMillis();
        public static List<List<int>> DataMapInit = new List<List<int>> { new List<int> { 3,640,408 },new List<int> { 27,756,336 },new List<int> { 28,684,312 }
        , new List<int> { 29,1034,360 }, new List<int>{6,485,336 },new List<int>{4,641,360 }};
        public static int CountSonTinhThuyTinh = 0;
        public static long DelaySpawn = 19000 + ServerUtils.CurrentTimeMillis();

        public static List<int> MapInitNpc = new List<int> { };
        public static List<int> ZoneInitNpc = new List<int> { };

        public static long DelayInit = 30000 + ServerUtils.CurrentTimeMillis();
        public static void ChooseMapInitNpc(long timeserver)
        {
            if (DelayInit < timeserver)
            {
                DelayInit = 1800000 + timeserver;
                MapInitNpc.Clear();
                ZoneInitNpc.Clear();
                for (int i = 0; i <= 3; i++)
                {
                    MapInitNpc.Add(DataMapInit[ServerUtils.RandomNumber(DataMapInit.Count)][0]);
                    ZoneInitNpc.Add(ServerUtils.RandomNumber(3, 19));
                    Server.Gi().Logger.Print("Map Init Npc 52: " + MapInitNpc[i] + " Zone: " + ZoneInitNpc[i], "cyan");
                }
            }
        }
        public static int GetZoneInitNpc(int mapId)
        {
            if (mapId == MapInitNpc[0])
            {
                return ZoneInitNpc[0];
            }
            else if (mapId == MapInitNpc[1])
            {
                return ZoneInitNpc[1];
            }
            else if (mapId == MapInitNpc[2])
            {
                return ZoneInitNpc[2];
            }
            return -1;
        }
        public static void Runtime(long timeserver)
        {

            ChooseMapInitNpc(timeserver);
            if (DelaySpawn < timeserver)
            {
                DelaySpawn = 35000 + timeserver;
                if (CountSonTinhThuyTinh <= 5)
                {
                    var randomIndex = ServerUtils.RandomNumber(DataMapInit.Count);
                    var MapInit = MapManager.Get(DataMapInit[randomIndex][0]);
                    var sontinh = new Boss();
                    var thuytinh = new Boss();
                    var randomZone = ServerUtils.RandomNumber(1, 10);
                    sontinh.CreateSonTinhThuyTinh(84, (short)DataMapInit[randomIndex][1], (short)DataMapInit[randomIndex][2], thuytinh, thuytinh.Id);
                    sontinh.CharacterHandler.SetUpInfo();
                    MapInit.Zones[randomZone].ZoneHandler.AddBoss(sontinh);

                    thuytinh.CreateSonTinhThuyTinh(83, (short)DataMapInit[randomIndex][1], (short)DataMapInit[randomIndex][2], sontinh, sontinh.Id);
                    thuytinh.CharacterHandler.SetUpInfo();
                    MapInit.Zones[randomZone].ZoneHandler.AddBoss(thuytinh);
                    CountSonTinhThuyTinh++;
                    Server.Gi().Logger.Print("SON TINH - THUY TINH: MAP[" + MapInit.Id + "] ZONE[" + randomZone + "]", "cyan");
                }
            }

        }
    }
}
