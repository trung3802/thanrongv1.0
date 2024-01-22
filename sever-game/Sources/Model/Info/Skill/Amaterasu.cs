using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Model.Info.Skill
{
    public class Amaterasu
    {
        public bool isAmaterasu { get; set; }
        public long timeAmaterasu { get; set; }
        public List<int> ListCharacterId { get; set; }
        public Amaterasu()
        {
            isAmaterasu = false;
            timeAmaterasu = -1;
            ListCharacterId = new List<int>();
        }
    }
}
