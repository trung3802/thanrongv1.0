using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TienKiemV2Remastered.Application.Interfaces.Map;
using TienKiemV2Remastered.DatabaseManager;
using TienKiemV2Remastered.Model.Character;
using TienKiemV2Remastered.Application.Interfaces.Character;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Application.Manager
{
    public class MapManager
    {
        public static int IdBase = 0;
        public static readonly ConcurrentDictionary<int, IMapCustom> Enrtys = new ConcurrentDictionary<int, IMapCustom>();
        private static readonly List<Threading.Map> Maps = new List<Threading.Map>();

        public static bool isDragonHasAppeared = false;
        public static long delayCallDragon = 600000;
        public static int IdPlayerCallDragon = -1;
        public static long timeUoc = -1;
        public static Task RuntimeGoiRong { get; set; }
        public static Threading.Map Get(int id)
        {
            return Maps.FirstOrDefault(x => x.Id == id);
        }

        public static IMapCustom GetMapCustom(int id)
        {
            lock (Enrtys)
            {
                return Enrtys.Values.FirstOrDefault(x => x.Id == id);  
            }
        }

        public static void InitMapServer()
        {
            Cache.Gi().TILE_MAPS.ForEach(x =>
            {
                Maps.Add(new Threading.Map(x.Id, x, null));
            });
        }

        public static void JoinMap(Character @char, int mapId, int zoneId, bool isDefault, bool isTeleport, int typeTeleport)
        {
            OutMap(@char, mapId);
            var map = Get(mapId);
            map?.JoinZone(@char, zoneId, isDefault, isTeleport, typeTeleport );
        }

        public static void OutMap(Character @char, int mapNextId)
        {
            var map = Get(@char.InfoChar.MapId);
            map?.OutZone(@char, mapNextId);
        }

        // For only once dragon apprea
        public static void SetDragonAppeared(bool toggle)
        {
            isDragonHasAppeared = toggle;
        }

        public static bool IsDragonHasAppeared()
        {
            return isDragonHasAppeared;
        }
    }
}