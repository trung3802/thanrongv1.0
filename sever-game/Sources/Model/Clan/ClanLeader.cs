using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Model.Clan
{
    public class ClanLeader
    {
        public short Head { get; set; }
        public short Body { get; set; }
        public short Leg { get; set; }
        public ClanLeader(short head, short body, short leg)
        {
            Head = head;
            Body = body;
            Leg = leg;
        }
    }
}
