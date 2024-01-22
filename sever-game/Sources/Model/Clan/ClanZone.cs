using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Model.Clan
{
    public class ClanZone
    {
        public IList<Application.Threading.Map> Maps { get; set; }
        public ClanZone()
        {
            Maps = new List<Application.Threading.Map>();
            Maps.Add(new Application.Threading.Map(153, tileMap: null, mapCustom: null));
            Maps.Add(new Application.Threading.Map(165, tileMap: null, mapCustom: null));
        }
        public Application.Threading.Map GetMapById(int mapid)
        {
            return Maps.FirstOrDefault(i => i.TileMap.Id == mapid);
        }
    }
}
