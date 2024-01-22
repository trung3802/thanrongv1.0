using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Application.Extension.NamecballWar
{
    public class NamecballWar
    {
        public static readonly List<string> NameTeam = new List<string> { "Ca Đíc", "Fide"};
        public static readonly List<long> CurrDaysOpen = new List<long> { 3, 5, 7 };
        public static readonly long currTimeRegister = 25; // 18 gio 25
        public static readonly long currTimeStart = 30; // 18 gio 30
        public static readonly List<List<String>> ListMenus = new List<List<String>>() { new List<string> { "Tranh ngọc\nrồng Namec" }, { new List<string> { "Hướng dẫn", "Đổi điểm\nthưởng\n[{0}]", "Bảng\nxếp hạng", "Từ chối" } } };
        public static NamecballWar_Info Cadic = new NamecballWar_Info();
        public static NamecballWar_Info Fide = new NamecballWar_Info();
        public static bool Open = false;
        public static bool Register = false;
        public static NamecballWar instance;
        public static NamecballWar gI()
        {
            if (instance == null) instance = new NamecballWar();
            return instance;
        }
        public void Update(DateTime time)
        {
            if (time.Hour == 18 && (time.Day is 3 or 5 or 7))
            {
                if (time.Minute >= currTimeRegister && !Register)
                {
                    Register = true;
                }
                if (time.Minute >= currTimeStart && !Open)
                {
                    Open = true;
                    Register = false;
                }
            }
           
        }
    }
}
