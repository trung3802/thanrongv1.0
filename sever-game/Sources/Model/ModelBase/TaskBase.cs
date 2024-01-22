using System.Collections.Generic;

namespace TienKiemV2Remastered.Model.ModelBase
{
    public class TaskBase
    {
        public short Id { get; set; }
        public List<string> SubNames { get; set; }
        public List<short> Counts { get; set; }
        public List<string> ContentInfo { get; set; }
        public long[] Reward { get; set; }
        public List<Item.ItemGiftcode> ItemReward { get; set; }
     
        public TaskBase()
        {
            SubNames = new List<string>();
            Counts = new List<short>();
            ContentInfo = new List<string>();
            Reward = new long[3];
            ItemReward = new List<Item.ItemGiftcode>();           
        }
    }
}