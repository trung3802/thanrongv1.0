using TienKiemV2Remastered.Application.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Application.Extension.Task_Everyday
{
    
    public class MissionData
    {
     
        public static List<String> EASY = new List<string> {"Hạ gục " + GetCount(0,0) + "Khỉ lông đỏ", "Lụm " + GetCount(0,1) +" vàng", "Hạ gục " + GetCount(0,2)+ " Boss","Hạ gục" + GetCount(0,3) + " người chơi khác"};
        public static List<String> NORMAL = new List<string> { "Hạ gục " + GetCount(1, 0) + "Khỉ lông đỏ", "Lụm " + GetCount(1, 1) + " vàng", "Hạ gục " + GetCount(1, 2) + " Boss", "Hạ gục" + GetCount(1, 3) + " người chơi khác" };
        public static List<String> HARD = new List<string> { "Hạ gục " + GetCount(2, 0) + "Khỉ lông đỏ", "Lụm " + GetCount(2, 1) + " vàng", "Hạ gục " + GetCount(2, 2) + " Boss", "Hạ gục" + GetCount(2, 3) + " người chơi khác" };
        public static int GetCount(int level, int type)
        {
            var count = ServerUtils.RandomNumber(0);
            if (level == 0)
            {
                if (type == 0) /// mob
                {
                    count = ServerUtils.RandomNumber(12, 17);
                }else if (type == 1)// gold lum
                {
                    count = ServerUtils.RandomNumber(2000, 8000);
                } else if (type == 2) // giet boss
                {
                    count = ServerUtils.RandomNumber(1);
                }else if (type == 3)
                {
                    count = ServerUtils.RandomNumber(5, 10);
                }
                return count;
            }
            if (level == 1)
            {
                if (type == 0) /// mob
                {
                    count = ServerUtils.RandomNumber(32, 57);
                }
                else if (type == 1)// gold lum
                {
                    count = ServerUtils.RandomNumber(1200000,1600000);
                }
                else if (type == 2) // giet boss
                {
                    count = ServerUtils.RandomNumber(2, 3);
                }
                else if (type == 3)
                {
                    count = ServerUtils.RandomNumber(10,16);
                }
                return count;
            }
            if (level == 2)
            {
                if (type == 0) /// mob
                {
                    count = ServerUtils.RandomNumber(120,170);
                }
                else if (type == 1)// gold lum
                {
                    count = ServerUtils.RandomNumber(45000000,50000000);
                }
                else if (type == 2) // giet boss
                {
                    count = ServerUtils.RandomNumber(3,6);
                }
                else if (type == 3)
                {
                    count = ServerUtils.RandomNumber(15,22);
                }
                return count;
            }
            return count;
        }
    }
}
