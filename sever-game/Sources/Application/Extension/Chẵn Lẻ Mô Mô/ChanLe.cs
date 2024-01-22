using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Application.Extension.Chẵn_Lẻ_Momo
{
    public class ChanLe
    {
        public Boolean pickChan{get;set;}
        public Boolean pickLe{get;set;}
        public int thoivang { get; set; }
        public ChanLe()
        {
            pickChan = false;
            pickLe = false;
            thoivang = 0;
        }
    }
}
