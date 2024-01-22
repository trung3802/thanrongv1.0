using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Model.Info.Skill
{
    public class MaPhongBa
    {
        public bool isMaPhongBa { get; set; }
        public long timeMaPhongBa { get; set; }
        public List<int> IdMonsterMaps { get; set; }
        public List<int> IdCharacters { get; set; }
        public List<int> IdBosses { get; set; }

        public MaPhongBa()
        {
            isMaPhongBa = false;
            timeMaPhongBa = -1;
            IdMonsterMaps = new List<int>();
            IdCharacters = new List<int>();
            IdBosses = new List<int>();
        }
    }
}
