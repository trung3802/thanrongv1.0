using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Application.Extension.ChampionShip.ChampionShip_23
{
    public class Menu
    {
        public static List<string> Text = new List<string> {"Đại hội võ thuật lần thứ 23\nDiễn ra bất kể ngày đêm, ngày nghỉ, ngày lễ\nPhần thưởng vô cùng quý giá\nNhanh chóng tham gia nào !" 
        ,"Phần thưởng của bạn đang ở cấp {0}\nMỗi ngày chỉ được nhận thưởng 1 lần\nbạn có chắc sẽ nhận thưởng ngay bây giờ?"};
        public static string textHuongDanThem = "Đại hội quy tụ nhiều cao thủ võ thuật như Jacky chun,Độc cô đế,Bang chủ cái bang,Bảo đẹp trai\nPhần thưởng là 1 rương gỗ chứa nhiều vp giá trị\nKhi hạ được 1 đối thủ,phần thưởng sẽ nâng lên 1 cấp\nRương càng cao cấp,vp trong đó sẽ càng giá trị hơn.";
        public static List<List<string>> TextMenu = new List<List<string>>()
        {
            new List<string>()
            {
                "Hướng dẫn thêm",
                "Thi đấu\n1 ngọc",
                "Thi đấu\n50 k\nvàng",
                "Về\nĐại hội\nvõ thuật"
            },
             new List<string>()
            {
            "Hướng dẫn thêm",
                "Thi đấu\n{0} ngọc",
                "Thi đấu\n{1}\nvàng",
                "Nhận\nthưởng\nRương cấp {2}",
                "Về\nĐại hội\nvõ thuật"
            },
             new List<string>()
            {
                 "OK",
             "Từ chối"
            }
        };
    }
}
