﻿using System.Collections.Generic;
using System.ComponentModel;

namespace TienKiemV2Remastered.Model.Info.Skill
{
    public class QCKK
    {
        public long Time { get; set; }
        public List<int> ListId { get; set; }

        public QCKK()
        {
            Time = -1;
            ListId = new List<int>();
        }
    }
}