using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Model.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Application.Extension.Yardat
{
    public class Yardat
    {
        public static void Init()
        {
            for (int i = 0; i < Cache.YaradatsMap.Count; i++)
            {
                var Yardat = MapManager.Get(Cache.YaradatsMap[i]);
                for (int zone = 0; zone < 20; zone++)
                {
                    for (int tanbinh = 0; tanbinh < Cache.Posistion[i].Count; tanbinh++)
                    {
                        var boss = new Boss();
                        boss.CreateBossYardat(Cache.YardatsEntity[i], "-"+tanbinh,(short)Cache.Posistion[i][tanbinh][0], (short)Cache.Posistion[i][tanbinh][1]);
                        boss.CharacterHandler.SetUpInfo();
                        Yardat.Zones[zone].ZoneHandler.AddBoss(boss);
                    }
                }
            }
        }
      
    }
}
