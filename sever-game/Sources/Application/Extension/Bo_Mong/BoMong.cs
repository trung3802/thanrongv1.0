using TienKiemV2Remastered.DatabaseManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Application.Extension.Bo_Mong
{
    public class BoMong
    {
        public List<long> Count { get; set; }
        public List<Boolean> isFinish { get; set; }
        public List<Boolean> isCollect { get; set; }
        public BoMong()
        {
            Count = new List<long>(Cache.Gi().TASK_BO_MONG.Count);
            isFinish = new List<Boolean>(Cache.Gi().TASK_BO_MONG.Count);
            isCollect = new List<Boolean>(Cache.Gi().TASK_BO_MONG.Count);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
         
        }
    }
}
