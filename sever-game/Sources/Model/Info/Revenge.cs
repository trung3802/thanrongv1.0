﻿using System.Collections.Generic;

namespace TienKiemV2Remastered.Model.Info
{
    public class Revenge
    {
        public bool IsRevenge { get; set; }
        public int EnemyId { get; set; }
        public long Time { get; set; }

        public Revenge()
        {
            EnemyId = -1;
            Time = -1;
        }
    }
}