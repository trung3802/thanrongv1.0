using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Model.Template
{
    public class ABossTemplate
    {
        public int Type { get; set; }
        public long TimeSpawn { get; set; }
        public long Delay { get; set; }
        public int[] Map { get; set; }
        public short[] X { get; set; }
        public short[] y { get; set; }
        public int Count { get; set; }
    }
}
