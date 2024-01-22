using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Application.Extension.ChampionShip.ChampionShip_23
{
    public class DataDaiHoiVoThuat23
    {
        public byte Round { get; set; }
        public Boolean Battled { get; set; }
        public Boolean isCollected { get; set; }
        public byte ChestLevel { get; set; }
        public byte Count { get; set; }
        public DataDaiHoiVoThuat23()
        {
            Round = 0;
            Battled = false;
            isCollected = false;
            Count = 0;
            ChestLevel = 0;
        }
        public void Clear()
        {
            Round = 0;
            Battled = false;
            isCollected = false;
            Count = 0;
            ChestLevel = 0;
        }
    }
}
