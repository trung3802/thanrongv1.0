using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Sources.Base.Template
{
    public class DiscipleTemplate
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public String Name { get; set; }
        public long Power { get; set; }
        public long Hp { get; set; }
        public long Mp { get; set; }
        public long Damage { get; set; }

        public long Defend { get; set; }
        public int Critical { get; set; }
        public int Head { get; set; }
        public int Body { get; set; }
        public int Leg { get; set; }
        public DiscipleTemplate()
        {
            Id = 0;
            Type = 0;
            Name = "Null";
            Power = 0;
            Hp = 0;
            Mp = 0;
            Damage = 0;
            Defend = 0;
            Critical = 0;
            Head = 0;
            Body = 0;
            Leg = 0;
        }

    }
}
