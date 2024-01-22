using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Application.Extension.Task_Everyday
{
    public class Mission
    {
        public int countChange { get; set; }
        public int countMission { get; set; }
        public string mission { get; set; }
        public int levelMission { get; set; }
        public string reward { get; set; }
        public int Counting { get; set; }
        public int IdMission { get; set; }
        public int typeMission { get; set; }
        public Mission()
        {
            countChange = 20;
            Counting = 0;
            countMission = -1;
            mission = "";
            levelMission = -1;
      //      reward = -1;
      //      typeReward = -1;
            IdMission = -1;
            typeMission = -1;
        }
    }
}
