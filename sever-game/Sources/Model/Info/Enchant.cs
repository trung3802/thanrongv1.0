using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Sources.Base.Info
{
    public class Enchant
    {
            
        public bool PhuHoMabu12h { get; set; }
        public bool PhuHoMabu2h { get; set; }
        public bool MiNuong { get; set; }
        public long timeMiNuong { get; set; }
        public Enchant()
        {
  
            PhuHoMabu12h = false;
            PhuHoMabu2h = false;
            MiNuong = false;
            timeMiNuong = -1;
        }
    }
}
