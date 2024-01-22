using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Model.Info.Skill
{
    public class HoaBang
    {
        public long Time { get; set; }
        public bool isHoaBang { get; set; }
        public HoaBang()
        {
            Time = -1;
            isHoaBang = false;
        }
    }
}
