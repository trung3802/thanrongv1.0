using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Application.Extension.Namecball
{
    public class NamecBall
    {
        public int Id { get; set; }
        public string MapName { get; set; }
        public int PlayerPick { get; set; }
        public int ZoneId { get; set; }
        public int MapId { get; set; }
        public short X { get; set; }
        public short Y { get; set; }
    }
}
