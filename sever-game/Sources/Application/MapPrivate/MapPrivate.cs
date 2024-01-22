using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.Model.Character;

namespace TienKiemV2Remastered.Application.MapPrivate
{
    public class MapPrivate
    {
        public IList<Threading.Map> Maps { get; set; }
        public MapPrivate(int gender)
        {
            Maps = new List<Threading.Map>();
            Maps.Add(new Threading.Map(45, tileMap: null, mapCustom: null));
            Maps.Add(new Threading.Map(46, tileMap: null, mapCustom: null));
            Maps.Add(new Threading.Map(47, tileMap: null, mapCustom: null));
            Maps.Add(new Threading.Map(48, tileMap: null, mapCustom: null));
            Maps.Add(new Threading.Map(49, tileMap: null, mapCustom: null));
            Maps.Add(new Threading.Map(50, null, null));
            Maps.Add(new Threading.Map(111, tileMap: null, mapCustom: null));
            Maps.Add(new Threading.Map(154, null, null));
            Maps.Add(new Threading.Map(21 + gender, null, null));
            Maps.Add(new Threading.Map(39 + gender, null, null));

        }
        public Threading.Map GetMapById(int id)
        {
            return Maps.FirstOrDefault(map => map.Id == id);
        }
        public void ExitMap(Character character,int mapOldId, int MapNextId)
        {
            
        }
        public int GetIndexMap(int mapId)
        {
            switch (mapId)
            {
                case 45: return 0;
                case 46: return 1;
                case 47: return 2;
                case 48: return 3;
                case 49: return 4;
                case 50: return 5;
                case 111: return 6;
                case 154: return 7;
            }
            return -1;
        }
    }
}
