using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Sources.Base.Info.Skill
{
    public class HoaDa
    {
        public bool IsHoaDa { get; set; }
        public long Time { get; set; }
        public int CharacterId { get; set; }
        public int Fight { get; set; }
        public int Percent { get; set; }

        public HoaDa()
        {
            IsHoaDa = false;
            Time = -1;
            CharacterId = -1;
            Fight = -1;
            Percent = 0;
        }
    }
}
