﻿using System.Collections.Generic;
using TienKiemV2Remastered.Model.Option;

namespace TienKiemV2Remastered.Model.Info.Radar
{
    public class Card
    {
        public short Id { get; set; }
        public int Amount { get; set; }
        public int MaxAmount { get; set; }
        public int Level { get; set; }
        public int Used { get; set; }
        public List<OptionRadar> Options { get; set; }

        public Card()
        {
            Id = -1;
            Amount = 0;
            MaxAmount = 0;
            Level = 0;
            Used = 0;
            Options = new List<OptionRadar>();
        }
    }
}